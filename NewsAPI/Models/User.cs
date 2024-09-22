using Microsoft.AspNetCore.Identity;

namespace NewsAPI.Models
{
    public class User : IdentityUser
    {
        #region Constructors
        public User()
        {
               
        }
        #endregion

        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; } // "Author" or "Reader"

        public Author Author { get; set; } // Navigation property to a single Author

        #endregion        

    }
}
