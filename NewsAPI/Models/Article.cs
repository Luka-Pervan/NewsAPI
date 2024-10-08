﻿using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;

namespace NewsAPI.Models
{
    public class Article
    {
        #region Constructors
        public Article()
        {
                
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        #endregion

    }
}
