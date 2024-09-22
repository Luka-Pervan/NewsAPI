using System.ComponentModel.DataAnnotations;

namespace NewsAPI.DTOs
{
    public class AuthorDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int AuthorUserId { get; set; }
        public string Bio { get; set; }

    }
}
