using System.Collections.Generic;
using WantList.Core;

namespace WantList.Data.Interfaces
{
    public interface IMangaData
    {
        IEnumerable<Manga> GetAll();
        Manga GetById(int id);
        Manga GetByMangaUpdatesId(int mangaUpdatesId);
        Manga Add(Manga manga);
        Manga Update(Manga manga);
        Manga Delete(int id);
        int Commit();
    }
}