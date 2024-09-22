using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs
{
    public class ArticleDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
