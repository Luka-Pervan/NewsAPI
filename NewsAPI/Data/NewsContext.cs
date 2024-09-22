using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;
using System.Collections.Generic;

namespace NewsAPI.Data
{
    public class NewsContext : IdentityDbContext<User>
    {
        #region Constructors
        public NewsContext(DbContextOptions<NewsContext> options) : base(options) { }

        #endregion

        #region Properties
        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }

        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API configurations for relationships, constraints, etc.
            modelBuilder.Entity<Article>()
                        .HasOne(a => a.Author)
                        .WithMany(b => b.Articles)
                        .HasForeignKey(a => a.AuthorId);

            modelBuilder.Entity<Author>()
                .HasOne(a => a.User)
                .WithOne() // Each User can have only one Author
                .HasForeignKey<Author>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: set behavior on user deletion

        }

        #endregion

    }

}
