namespace WantList.MangaUpdates.Data
{
    public class Manga
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int Volumes { get; set; }
        public bool Completed { get; set; }
    }
}