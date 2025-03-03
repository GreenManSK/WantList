using WantList.MangaUpdates.Data;

namespace WantList.MangaUpdates
{
    public interface IMangaUpdatesService
    {
        string GetImageName(string mangaUpdatesId);
        string GetImagePath(string mangaUpdatesId);
        byte[] DownloadImage(Manga manga);
        Manga GetData(string mangaUpdatesId);
    }
}