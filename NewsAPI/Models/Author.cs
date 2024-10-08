﻿namespace NewsAPI.Models
{
    public class Author
    {
        #region Constructors
        public Author()
        {
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public List<Article> Articles { get; set; }

        public int AuthorUserId { get; set; }
        public User User { get; set; }

        #endregion

    }
}
