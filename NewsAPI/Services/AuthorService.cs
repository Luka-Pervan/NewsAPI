using NewsAPI.Data;
using NewsAPI.Models;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Shared;
using NewsAPI.DTOs;

namespace NewsAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly NewsContext _context;

        public AuthorService(NewsContext context)
        {
            _context = context;
        }

        // Fetch all authors
        public async Task<Result<IEnumerable<Author>>> GetAllAuthorsAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            return Result<IEnumerable<Author>>.Success(authors);
        }

        // Fetch author by ID
        public async Task<Result<Author>> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors.Include(a => a.Articles)
                                               .FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
                return Result<Author>.Failure($"Author with id {id} not found.");
            return Result<Author>.Success(author);
        }

        // Create a new author
        public async Task<Result<Author>> CreateAuthorAsync(AuthorDTO authorDto)
        {
            var newAuthor = new Author
            {
                AuthorUserId = authorDto.AuthorUserId,
                Name = authorDto.Name,
                Bio = authorDto.Bio
            };

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();
            return Result<Author>.Success(newAuthor);
        }

        // Update an existing author
        public async Task<Result> UpdateAuthorAsync(int id, AuthorDTO authorDto)
        {
            var existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null)
                return Result.Failure($"Author with id {id} not found.");

            existingAuthor.Name = authorDto.Name;
            existingAuthor.Bio = authorDto.Bio;

            _context.Authors.Update(existingAuthor);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        // Delete an author
        public async Task<Result> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return Result.Failure($"Author with id {id} not found.");

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
