using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WantList.Anidb
{
    public class AnidbService
    {
        private readonly ILogger<AnidbService> _logger;
        private readonly string _imagesPath;

        public AnidbService(ILogger<AnidbService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _imagesPath = configuration.GetValue<string>("ImagesPath");
        }

        public string GetImageName(int anidbId)
        {
            return $"a{anidbId}.jpg";
        }

        public string GetImagePath(int anidbId)
        {
            return Path.Combine(_imagesPath, GetImageName(anidbId));
        }

        public void DownloadImage(int anidbId)
        {
            var imagePath = GetImagePath(anidbId);
            if (File.Exists(imagePath))
            {
                _logger.LogError($"Image for anidb {anidbId} already exists in {imagePath}");
                return;
            }

            var html = GetAnimeHtml(anidbId).Result;
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var image = htmlDoc.DocumentNode.Descendants("img").FirstOrDefault();
            if (image == null)
            {
                _logger.LogError($"Couldn't find image for anidb {anidbId}");
                return;
            }

            var imageUrl = image.GetAttributeValue("src", "");
            _logger.LogInformation($"Downloading file {imageUrl} to {imagePath} for anidb {anidbId}");
            using (var client = new WebClient())
            {
                client.DownloadFile(imageUrl, imagePath);
            }
        }

        public void DeleteImage(int anidbId)
        {
            var path = GetImagePath(anidbId);
            if (File.Exists(path))
            {
                _logger.LogInformation($"Deleting {path}");
                File.Delete(path);
            }
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