using NewsAPI.Data;
using NewsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace NewsAPI.Services
{
    public class ArticleService : IArticleService
    {
        private readonly NewsContext _context;

        public ArticleService(NewsContext context)
        {
            _context = context;
        }

        // Fetch all articles
        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles.Include(a => a.Author).ToListAsync();
        }

        // Fetch article by ID
        public async Task<Article> GetArticleByIdAsync(int id)
        {
            return await _context.Articles.Include(a => a.Author)
                                          .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Create a new article
        public async Task<Article> CreateArticleAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        // Update an existing article
        public async Task<bool> UpdateArticleAsync(int id, Article article)
        {
            var existingArticle = await _context.Articles.FindAsync(id);
            if (existingArticle == null) return false;

            existingArticle.Title = article.Title;
            existingArticle.Content = article.Content;
            existingArticle.AuthorId = article.AuthorId;

            _context.Articles.Update(existingArticle);
            await _context.SaveChangesAsync();

            return true;
        }

        // Delete an article
        public async Task<bool> DeleteArticleAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return false;

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
