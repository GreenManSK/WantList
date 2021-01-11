using Microsoft.EntityFrameworkCore;
using WantList.Core;
using WantList.Data.Interfaces;

namespace WantList.Data.Sql
{
    public class SqlSettingsData : ISettingsData
    {
        private readonly WantListDbContext _db;

        public SqlSettingsData(WantListDbContext db)
        {
            _db = db;
        }

        public Settings Get()
        {
            return _db.Settings.Find(1) ?? new Settings();
        }

        public Settings Update(Settings settings)
        {
            var entity = _db.Settings.Attach(settings);
            entity.State = EntityState.Modified;
            return settings;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}