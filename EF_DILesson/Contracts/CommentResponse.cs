namespace EF_DILesson.Contracts
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string AuthorName { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}