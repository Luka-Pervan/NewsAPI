using NewsAPI.Data;
using NewsAPI.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.Include(a => a.Articles).ToListAsync();
        }

        // Fetch author by ID
        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.Include(a => a.Articles)
                                         .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Create a new author
        public async Task<Author> CreateAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        // Update an existing author
        public async Task<bool> UpdateAuthorAsync(int id, Author author)
        {
            var existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null) return false;

            existingAuthor.Name = author.Name;

            _context.Authors.Update(existingAuthor);
            await _context.SaveChangesAsync();

            return true;
        }

        // Delete an author
        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
