using System;

namespace WantList.Core
{
    public class Manga
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MangaUpdatesId { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string Image { get; set; }
        public int WantRank { get; set; }
        public string MissingVolumes { get; set; }
    }
}