using System.ComponentModel.DataAnnotations;

namespace WantList.Core
{
    public class AnidbAnime
    {
        [Key]
        public int AnidbId { get; set; }
        public string Japanese { get; set; }
        public string English { get; set; }
    }
}