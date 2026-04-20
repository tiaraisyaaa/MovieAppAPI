namespace MovieappAPI.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public decimal Rating { get; set; }
    }
}
