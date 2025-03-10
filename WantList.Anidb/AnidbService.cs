using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WantList.Anidb.Data;
using WantList.Core;

namespace WantList.Anidb
{
    public class AnidbService
    {
        private const string EpisodeCountSelector = "//span[contains(@itemprop, 'numberOfEpisodes')]";
        private const string StartDateSelector = "//span[contains(@itemprop, 'startDate')]";
        private const string PublishedDateSelector = "//span[contains(@itemprop, 'datePublished')]";

        private readonly ILogger<AnidbService> _logger;
        private readonly string _imagesPath;

        public AnidbService(ILogger<AnidbService> logger, IConfiguration configuration)
        {
            _logger = logger;
        }

        public string GetImageName(int anidbId)
        {
            return $"a{anidbId}.jpg";
        }

        public string GetImagePath(int anidbId)
        {
            return Path.Combine(_imagesPath, GetImageName(anidbId));
        }

        public byte[] DownloadImage(AnimeData animeData)
        {
            var imageUrl = animeData.ImageUrl;
            _logger.LogInformation($"Downloading file {imageUrl} for anidb {animeData.Id}");
            using var client = new WebClient();
            var imageData = client.DownloadData(imageUrl);
            return imageData;
        }

        public AnimeData GetData(int anidbId)
        {
            var animeData = new AnimeData();
            animeData.Id = anidbId;

            var html = GetAnimeHtml(anidbId).Result;
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var imageNode = htmlDoc.DocumentNode.Descendants("img").FirstOrDefault();
            var episodeCountNode = htmlDoc.DocumentNode.SelectNodes(EpisodeCountSelector);
            var startDateNode = htmlDoc.DocumentNode.SelectNodes(StartDateSelector);
            var publishedDateNode = htmlDoc.DocumentNode.SelectNodes(PublishedDateSelector);

            animeData.ImageUrl = imageNode != null ? imageNode.GetAttributeValue("src", "") : "";
            if (episodeCountNode != null)
            {
                var node = episodeCountNode.First();
                animeData.EpisodeCount = int.Parse(node.InnerText);
                animeData.Type = GetAnimeType(node.ParentNode.InnerText);
            }

            if (startDateNode != null)
            {
                var date = startDateNode.First().InnerText;
                DateTime dateObj;
                if (DateTime.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out dateObj))
                {
                    animeData.ReleaseDate = dateObj;
                }
            }
            else if (publishedDateNode != null)
            {
                var date = publishedDateNode.First().InnerText;
                DateTime dateObj;
                if (DateTime.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out dateObj))
                {
                    animeData.ReleaseDate = dateObj;
                }
                animeData.Type = AnimeType.Movie;
                animeData.EpisodeCount = 1;
            }

            return animeData;
        }

        private AnimeType GetAnimeType(string desc)
        {
            if (desc.Contains("TV Series") || desc.Contains("Web"))
            {
                return AnimeType.Series;
            }

            if (desc.Contains("Movie"))
            {
                return AnimeType.Movie;
            }

            return AnimeType.OVA;
        }

        private async Task<string> GetAnimeHtml(int anidbId)
        {
            var url = GetUrl(anidbId);
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept",
                "text/html,application/xhtml+xml,application/xml");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

            _logger.LogInformation($"Downloading page {url}");
            return await client.GetStringAsync(url);
        }

        private string GetUrl(int anidbId)
        {
            return $"https://anidb.net/anime/{anidbId}";
        }
    }
}