using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WantList.MangaUpdates.Data;

namespace WantList.MangaUpdates
{
    public class MangaUpdatesService : IMangaUpdatesService
    {
        private const string TitleSelector = "//span[contains(@class, 'releasestitle')]";
        private const string ImageSelector = "//div[contains(@class, 'sContent')]//img";
        private const string VolumesSelector = "//div[contains(@class, 'sContent')][contains(., 'Volumes')]";

        private readonly Regex VolumesLineRegex = new Regex(@"^\d+ Volumes?");

        private readonly ILogger<MangaUpdatesService> _logger;
        private readonly string _imagesPath;

        public MangaUpdatesService(ILogger<MangaUpdatesService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _imagesPath = configuration.GetValue<string>("ImagesPath");
        }

        public string GetImageName(int mangaUpdatesId)
        {
            return $"m{mangaUpdatesId}.jpg";
        }

        public string GetImagePath(int mangaUpdatesId)
        {
            return Path.Combine(_imagesPath, GetImageName(mangaUpdatesId));
        }

        public void DownloadImage(Manga manga)
        {
            var imagePath = GetImagePath(manga.Id);
            if (File.Exists(imagePath))
            {
                _logger.LogError($"Image for manga {manga.Id} already exists in {imagePath}");
                return;
            }

            _logger.LogInformation($"Downloading file {manga.ImageUrl} to {imagePath} for manga {manga.Id}");
            using var client = new WebClient();
            client.DownloadFile(manga.ImageUrl, imagePath);
        }

        public void DeleteImage(int mangaUpdatesId)
        {
            var path = GetImagePath(mangaUpdatesId);
            if (File.Exists(path))
            {
                _logger.LogInformation($"Deleting {path}");
                File.Delete(path);
            }
        }

        public Manga GetData(int mangaUpdatesId)
        {
            var manga = new Manga();
            manga.Id = mangaUpdatesId;
            var html = GetHtml(mangaUpdatesId).Result;
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var titleElement = htmlDoc.DocumentNode.SelectNodes(TitleSelector);
            var imageElement = htmlDoc.DocumentNode.SelectNodes(ImageSelector);
            var volumesElement = htmlDoc.DocumentNode.SelectNodes(VolumesSelector);

            manga.Title = titleElement != null ? titleElement.First().InnerText : "";
            manga.ImageUrl = imageElement != null ? imageElement.First().GetAttributeValue("src", "") : "";

            if (volumesElement != null)
            {
                var lines = volumesElement.First().InnerHtml.Split("<br>");
                foreach (var line in lines)
                {
                    if (VolumesLineRegex.IsMatch(line))
                    {
                        manga.Volumes = int.Parse(line.Split(" ").First());
                        manga.Completed = line.Contains("Complete");
                        break;
                    }
                }
            }

            return manga;
        }

        private async Task<string> GetHtml(int mangaUpdatesId)
        {
            var url = GetUrl(mangaUpdatesId);
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept",
                "text/html,application/xhtml+xml,application/xml");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

            _logger.LogInformation($"Downloading page {url}");
            return await client.GetStringAsync(url);
        }

        private string GetUrl(in int mangaUpdatesId)
        {
            return $"https://www.mangaupdates.com/series.html?id={mangaUpdatesId}";
        }
    }
}