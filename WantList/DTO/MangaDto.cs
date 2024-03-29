using System;
using System.ComponentModel.DataAnnotations;

namespace WantList.DTO
{
    public class MangaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required] public string MangaUpdatesId { get; set; }
        public DateTime AddedDateTime { get; set; }
        [Required] public int WantRank { get; set; }
        public string MissingVolumes { get; set; }
        public bool Completed { get; set; }
        public bool Deleted { get; set; }
    }
}