using System;

namespace WantList.Core
{
    public class Manga
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MangaUpdatesId { get; set; }
        public DateTime AddedDateTime { get; set; }
        public int WantRank { get; set; }
        public string MissingVolumes { get; set; }
        public bool Completed { get; set; }
        public bool Deleted { get; set; }
        public byte[] Image { get; set; }
    }
}