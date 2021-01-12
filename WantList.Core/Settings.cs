using System;

namespace WantList.Core
{
    public class Settings
    {
        public int Id { get; set; }
        public DateTime AnidbLastSync { get; set; }

        public Settings(DateTime anidbLastSync)
        {
            AnidbLastSync = anidbLastSync;
        }
    }
}