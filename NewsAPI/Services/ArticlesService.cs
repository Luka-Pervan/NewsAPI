using NewsAPI.Data;
using NewsAPI.Models;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DTOs;
using NewsAPI.Shared;

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
        public async Task<Result<IEnumerable<Article>>> GetAllArticlesAsync()
        {
            try
            {
                var articles = await _context.Articles.Include(a => a.Author).ToListAsync();
                return Result<IEnumerable<Article>>.Success(articles);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Article>>.Failure($"An error occurred while retrieving articles: {ex.Message}");
            }
        }

        // Fetch article by ID
        public async Task<Result<Article>> GetArticleByIdAsync(int id)
        {
            try
            {
                var article = await _context.Articles.Include(a => a.Author)
                                                     .FirstOrDefaultAsync(a => a.Id == id);
                if (article == null)
                {
                    return Result<Article>.Failure("Article not found.");
                }
                return Result<Article>.Success(article);
            }
            catch (Exception ex)
            {
                return Result<Article>.Failure($"An error occurred while retrieving the article: {ex.Message}");
            }
        }

        // Create a new article
        public async Task<Result<Article>> CreateArticleAsync(ArticleDto articleDto)
        {
            try
            {
                var article = new Article
                {
                    Title = articleDto.Title,
                    Content = articleDto.Content,
                    AuthorId = articleDto.AuthorId,
                    PublishedDate = articleDto.PublishedDate ?? DateTime.UtcNow
                };

                _context.Articles.Add(article);
                await _context.SaveChangesAsync();

                return Result<Article>.Success(article);
            }
            catch (Exception ex)
            {
                return Result<Article>.Failure($"An error occurred while creating the article: {ex.Message}");
            }
        }

        // Update an existing article
        public async Task<Result> UpdateArticleAsync(int id, ArticleDto articleDto)
        {
            try
            {
                var existingArticle = await _context.Articles.FindAsync(id);
                if (existingArticle == null)
                {
                    return Result.Failure("Article not found.");
                }

                // Map the fields from the DTO
                existingArticle.Title = articleDto.Title;
                existingArticle.Content = articleDto.Content;
                existingArticle.AuthorId = articleDto.AuthorId;

                if (articleDto.PublishedDate.HasValue)
                {
                    existingArticle.PublishedDate = articleDto.PublishedDate.Value;
                }

                _context.Articles.Update(existingArticle);
                await _context.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while updating the article: {ex.Message}");
            }
        }

        // Delete an article
        public async Task<Result> DeleteArticleAsync(int id)
        {
            try
            {
                var article = await _context.Articles.FindAsync(id);
                if (article == null)
                {
                    return Result.Failure("Article not found.");
                }

                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while deleting the article: {ex.Message}");
            }
        }

    }
}
