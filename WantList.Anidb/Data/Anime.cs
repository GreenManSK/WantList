#nullable enable
namespace WantList.Anidb.Data
{
    public class Anime
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string JpName { get; set; }

        public Anime(int id, string enName, string jpName)
        {
            Id = id;
            EnName = enName;
            JpName = jpName;
        }
    }
}