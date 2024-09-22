using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Models;
using NewsAPI.Services;

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var result = await _authorService.GetAllAuthorsAsync();

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data); // Return the list of authors
        }

        // GET: api/authors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var result = await _authorService.GetAuthorByIdAsync(id);

            if (!result.Succeeded)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data); // Return the author object
        }

        // POST: api/authors
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO authorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authorService.CreateAuthorAsync(authorDto);

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return CreatedAtAction(nameof(GetAuthorById), new { id = result.Data.Id }, result.Data);
        }

        // PUT: api/authors/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDTO authorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authorService.UpdateAuthorAsync(id, authorDto);

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return NoContent(); // Update successful
        }

        // DELETE: api/authors/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthorAsync(id);

            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return NoContent(); // Deletion successful
        }

    }
}
