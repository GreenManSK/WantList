using System.Collections.Generic;
using WantList.Core;

namespace WantList.Data.Interfaces
{
    public interface IAnidbAnimeData
    {
        IEnumerable<AnidbAnime> GetAll();
        AnidbAnime Add(Anime AnidbAnime);
        AnidbAnime Update(Anime AnidbAnime);
        AnidbAnime Delete(int id);
        int Commit();
    }
}