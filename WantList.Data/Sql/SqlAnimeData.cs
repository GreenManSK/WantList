using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WantList.Core;
using WantList.Data.Interfaces;

namespace WantList.Data.Sql
{
    public class SqlAnimeData : IAnimeData
    {
        private readonly WantListDbContext _db;

        public SqlAnimeData(WantListDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Anime> GetAll()
        {
            return from a in _db.Animes select a;
        }

        public Anime GetById(int id)
        {
            return _db.Animes.Find(id);
        }

        public Anime Add(Anime anime)
        {
            _db.Add(anime);
            return anime;
        }

        public Anime Update(Anime anime)
        {
            var entity = _db.Animes.Attach(anime);
            entity.State = EntityState.Modified;
            return anime;
        }

        public Anime Delete(int id)
        {
            var anime = GetById(id);
            if (anime != null)
            {
                _db.Animes.Remove(anime);
            }

            return anime;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}