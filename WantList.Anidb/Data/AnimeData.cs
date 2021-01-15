using System;
using WantList.Core;

namespace WantList.Anidb.Data
{
    public class AnimeData
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int EpisodeCount { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public AnimeType? Type { get; set; }
    }
}