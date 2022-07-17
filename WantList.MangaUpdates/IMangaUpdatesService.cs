using WantList.MangaUpdates.Data;

namespace WantList.MangaUpdates
{
    public interface IMangaUpdatesService
    {
        string GetImageName(string mangaUpdatesId);
        string GetImagePath(string mangaUpdatesId);
        void DownloadImage(Manga manga);
        void DeleteImage(string mangaUpdatesId);
        Manga GetData(string mangaUpdatesId);
    }
}