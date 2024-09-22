namespace NewsAPI.DTOs
{
    public class UserRegistrationDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Author" or "Reader"

    }
}
