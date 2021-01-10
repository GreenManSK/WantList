using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WantList.Core;
using WantList.Data.Interfaces;

namespace WantList.Data.Sql
{
    public class SqlAnidbAnimeData : IAnidbAnimeData
    {
        private readonly WantListDbContext _db;

        public SqlAnidbAnimeData(WantListDbContext db)
        {
            _db = db;
        }

        public IEnumerable<AnidbAnime> GetAll()
        {
            return from a in _db.AnidbAnimes select a;
        }

        public AnidbAnime GetById(int id)
        {
            return _db.AnidbAnimes.Find(id);
        }

        public AnidbAnime Add(AnidbAnime anidbAnime)
        {
            _db.Add(anidbAnime);
            return anidbAnime;
        }

        public AnidbAnime Update(AnidbAnime anidbAnime)
        {
            var entity = _db.AnidbAnimes.Attach(anidbAnime);
            entity.State = EntityState.Modified;
            return anidbAnime;
        }

        public AnidbAnime Delete(int id)
        {
            var anidbAnime = GetById(id);
            if (anidbAnime != null)
            {
                _db.AnidbAnimes.Remove(anidbAnime);
            }

            return anidbAnime;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}