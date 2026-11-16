namespace EF_DILesson.Models
{
    public class MovieComment
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Film Film { get; set; }
        public string AuthorName { get; set; } = "";
        public string Text { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}