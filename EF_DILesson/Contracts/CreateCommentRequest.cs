namespace EF_DILesson.Contracts
{
    public class CreateCommentRequest
    {
        public string AuthorName { get; set; } = "";
        public string Text { get; set; } = "";
    }
}