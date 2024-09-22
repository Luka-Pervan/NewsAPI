using NewsAPI.Models;

namespace NewsAPI.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();          // Fetch all authors
        Task<Author> GetAuthorByIdAsync(int id);                // Fetch author by ID
        Task<Author> CreateAuthorAsync(Author author);          // Create a new author
        Task<bool> UpdateAuthorAsync(int id, Author author);    // Update an existing author
        Task<bool> DeleteAuthorAsync(int id);                   // Delete an author
    }
}
