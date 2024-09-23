using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;

namespace NewsAPI.Data
{
    public class NewsContext : IdentityDbContext<User, IdentityRole<int>, int>
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

            modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

            // Article -> Author relationship
            modelBuilder.Entity<Article>()
                        .HasOne(a => a.Author)
                        .WithMany(b => b.Articles)   // An author can write many articles
                        .HasForeignKey(a => a.AuthorId)
                        .OnDelete(DeleteBehavior.Cascade);  // If the author is deleted, articles should also be deleted

            // Author -> User relationship (Each Author is a User)
            modelBuilder.Entity<Author>()
                .HasOne(a => a.User)              // One-to-one relationship with User
                .WithOne(u => u.Author)           // User has a single Author associated
                .HasForeignKey<Author>(a => a.AuthorUserId) // Foreign key in Author table referencing User
                .OnDelete(DeleteBehavior.Restrict);  // Avoid deleting user when an author is deleted

        }


        #endregion

    }

}
