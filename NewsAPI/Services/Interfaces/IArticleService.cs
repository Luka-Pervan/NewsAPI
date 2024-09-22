using NewsAPI.Models;

namespace NewsAPI.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAllArticlesAsync();          // Fetch all articles
        Task<Article> GetArticleByIdAsync(int id);                // Fetch article by ID
        Task<Article> CreateArticleAsync(Article article);        // Create a new article
        Task<bool> UpdateArticleAsync(int id, Article article);   // Update an existing article
        Task<bool> DeleteArticleAsync(int id);                    // Delete an article

    }
}
