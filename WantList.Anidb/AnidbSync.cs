using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.GZip;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WantList.Core;
using WantList.Data.Interfaces;
using Anime = WantList.Anidb.Data.Anime;

namespace WantList.Anidb
{
    public class AnidbSync
    {
        private const string DataFile = "anidbData.gz";

        private readonly XName _languageAttrName = XName.Get("lang", "http://www.w3.org/XML/1998/namespace");
        private readonly ILogger<AnidbSync> _logger;
        private ISettingsData _settingsData;
        private IAnidbAnimeData _anidbAnimeData;
        private readonly int _intervalInDays;
        private readonly string _anidbUrl;

        public AnidbSync(ILogger<AnidbSync> logger, IConfiguration configuration, ISettingsData settingsData, IAnidbAnimeData anidbAnimeData)
        {
            _logger = logger;
            _settingsData = settingsData;
            _anidbAnimeData = anidbAnimeData;
            var anidbConfig = configuration.GetSection("Anidb");
            _intervalInDays = anidbConfig.GetValue<int>("IntervalInDays");
            _anidbUrl = anidbConfig.GetValue<string>("Url");
        }

        public void OnStartup()
        {
            try
            {
                if (ShouldUpdate())
                {
                    _logger.LogInformation("Updating anidb database");
                    var data = GetData();
                    var parsedData = ParseData(data).ToArray();
                    var anidbAnime = GetAnime();
                    AddNew(parsedData, anidbAnime);
                    UpdateOld(parsedData, anidbAnime);
                    UpdateLastSync();
                    _logger.LogInformation("Anidb database updated");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while updating anidb database");
            }
        }

        private void AddNew(IEnumerable<Anime> parsedData, Dictionary<int, AnidbAnime> anidbAnimes)
        {
            foreach (var parsedAnime in parsedData)
            {
                if (anidbAnimes.ContainsKey(parsedAnime.Id)) continue;
                var anime = new AnidbAnime()
                {
                    AnidbId = parsedAnime.Id,
                    Japanese = parsedAnime.JpName ?? "",
                    English = parsedAnime.EnName ?? ""
                };
                _anidbAnimeData.Add(anime);
            }

            _anidbAnimeData.Commit();
        }

        private void UpdateOld(IEnumerable<Anime> parsedData, Dictionary<int, AnidbAnime> anidbAnimes)
        {
            foreach (var parsedAnime in parsedData)
            {
                if (!anidbAnimes.ContainsKey(parsedAnime.Id)) continue;
                var anime = anidbAnimes[parsedAnime.Id];
                if (anime.English.Equals(parsedAnime.EnName) && anime.Japanese.Equals(parsedAnime.JpName)) continue;
                anime.English = parsedAnime.EnName;
                anime.Japanese = parsedAnime.JpName;
                _anidbAnimeData.Update(anime);
            }

            _anidbAnimeData.Commit();
        }

        private Dictionary<int, AnidbAnime> GetAnime()
        {
            return _anidbAnimeData.GetAll().ToDictionary(a => a.AnidbId, a => a);
        }

        private string GetData()
        {
            string content;
            using (var client = new WebClient())
            {
                client.DownloadFile(_anidbUrl, DataFile);
                using var fs = new FileStream(DataFile, FileMode.Open, FileAccess.Read);
                using var gzipStream = new GZipInputStream(fs);
                using var reader = new StreamReader(gzipStream, Encoding.UTF8);
                content = reader.ReadToEnd();
            }

            File.Delete(DataFile);

            return content;
        }

        private IEnumerable<Anime> ParseData(string data)
        {
            var xml = XDocument.Parse(data);
            return from a in xml.Root.Descendants("anime")
                select new Anime(
                    (int) a.Attribute("aid"),
                    (from title in a.Descendants("title")
                        let lang = title.Attribute(_languageAttrName)?.Value
                        let type = title.Attribute("type")?.Value
                        where lang != null && lang.Equals("en") && type != null && type.Equals("official")
                        select title.Value).FirstOrDefault() ?? "",
                    (from title in a.Descendants("title")
                        let val = title.Attribute("type")?.Value
                        where val != null && val.Equals("main")
                        select title.Value).FirstOrDefault() ?? ""
                );
        }

        private bool ShouldUpdate()
        {
            var lastUpdate = _settingsData.Get().AnidbLastSync;
            return (DateTime.Now - lastUpdate).TotalDays > _intervalInDays;
        }

        private void UpdateLastSync()
        {
            var settings = _settingsData.Get();
            settings.AnidbLastSync = DateTime.Now;
            _settingsData.Update(settings);
            _settingsData.Commit();
        }
    }
}