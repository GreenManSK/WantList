using WantList.MangaUpdates.Data;

namespace WantList.MangaUpdates
{
    public interface IMangaUpdatesService
    {
        string GetImageName(int mangaUpdatesId);
        string GetImagePath(int mangaUpdatesId);
        void DownloadImage(Manga manga);
        void DeleteImage(int mangaUpdatesId);
        Manga GetData(int mangaUpdatesId);
    }
}