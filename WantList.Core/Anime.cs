using System;

namespace WantList.Core
{
    public class Anime
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AnidbId { get; set; }
        public DateTime AddedDateTime { get; set; }
        public AnimeType Type { get; set; }
        public int WantRank { get; set; }
        public bool Redownload { get; set; }
        public bool BluRay { get; set; }
        public Quality Quality { get; set; }
        public string BluRayRelease { get; set; }
    }
}