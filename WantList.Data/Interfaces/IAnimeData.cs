using System.Collections.Generic;
using WantList.Core;

namespace WantList.Data.Interfaces
{
    public interface IAnimeData
    {
        IEnumerable<Anime> GetAll();
        Anime GetById(int id);
        Anime Add(Anime anime);
        Anime Update(Anime anime);
        Anime Delete(int id);
        int Commit();
    }
}