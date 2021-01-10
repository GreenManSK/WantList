using System.Collections.Generic;
using WantList.Core;

namespace WantList.Data.Interfaces
{
    public interface IAnidbAnimeData
    {
        IEnumerable<AnidbAnime> GetAll();
        AnidbAnime GetById(int id);
        AnidbAnime Add(AnidbAnime anidbAnime);
        AnidbAnime Update(AnidbAnime anidbAnime);
        AnidbAnime Delete(int id);
        int Commit();
    }
}