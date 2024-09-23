using NewsAPI.DTOs;
using NewsAPI.Models;
using NewsAPI.Shared;

namespace NewsAPI.Services
{
    public interface IAuthorService
    {
        Task<Result<IEnumerable<Author>>> GetAllAuthorsAsync();         // Fetch all authors
        Task<Result<Author>> GetAuthorByIdAsync(int id);               // Fetch author by ID
        Task<Result<Author>> CreateAuthorAsync(AuthorDTO authorDto);   // Create a new author
        Task<Result> UpdateAuthorAsync(int id, AuthorDTO authorDto);   // Update an existing author
        Task<Result> DeleteAuthorAsync(int id);                        // Delete an author
    }
}
