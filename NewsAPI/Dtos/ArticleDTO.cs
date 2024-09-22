namespace NewsAPI.DTOs
{
    public class ArticleDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
