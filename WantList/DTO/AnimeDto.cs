using System;
using System.ComponentModel.DataAnnotations;
using WantList.Core;

namespace WantList.DTO
{
    public class AnimeDto
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int AnidbId { get; set; }
        public DateTime AddedDateTime { get; set; }
        public DateTime ReleaseDate { get; set; }
        public AnimeType Type { get; set; }
        public int EpisodeCount { get; set; }
        [Required] public int WantRank { get; set; }
        public bool Redownload { get; set; }
        public bool BluRay { get; set; }
        public Quality Quality { get; set; }
        public string BluRayRelease { get; set; }
    }
}