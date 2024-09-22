using NewsAPI.DTOs;
using NewsAPI.Models;
using NewsAPI.Shared;

namespace NewsAPI.Services
{
    public interface IArticleService
    {
        Task<Result<IEnumerable<Article>>> GetAllArticlesAsync();  // Fetch all articles
        Task<Result<Article>> GetArticleByIdAsync(int id);         // Fetch article by ID
        Task<Result<Article>> CreateArticleAsync(ArticleDto articleDto); // Create a new article using DTO
        Task<Result> UpdateArticleAsync(int id, ArticleDto articleDto);  // Update an existing article using DTO
        Task<Result> DeleteArticleAsync(int id);                    // Delete an article

    }
}
