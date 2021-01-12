using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WantList.Core;
using WantList.Data.Interfaces;

namespace WantList.Data.Sql
{
    public class SqlMangaData : IMangaData
    {
        private readonly WantListDbContext _db;

        public SqlMangaData(WantListDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Manga> GetAll()
        {
            return from m in _db.Mangas select m;
        }

        public Manga GetById(int id)
        {
            return _db.Mangas.Find(id);
        }

        public Manga GetByMangaUpdatesId(int mangaUpdatesId)
        {
            return (from m in _db.Mangas where m.MangaUpdatesId.Equals(mangaUpdatesId) select m).FirstOrDefault();
        }

        public Manga Add(Manga manga)
        {
            _db.Add(manga);
            return manga;
        }

        public Manga Update(Manga manga)
        {
            var entity = _db.Mangas.Attach(manga);
            entity.State = EntityState.Modified;
            return manga;
        }

        public Manga Delete(int id)
        {
            var manga = GetById(id);
            if (manga != null)
            {
                _db.Mangas.Remove(manga);
            }

            return manga;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}