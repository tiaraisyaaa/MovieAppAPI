namespace MovieappAPI.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int ReleaseYear { get; set; }
    }
}
