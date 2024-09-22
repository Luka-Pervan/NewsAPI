﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Models;
using NewsAPI.Services;

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<User> _userManager;

        public ArticlesController(IArticleService articleService, UserManager<User> userManager)
        {
            _articleService = articleService;
            _userManager = userManager;
        }

        // GET: api/articles
        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var result = await _articleService.GetAllArticlesAsync();
            return Ok(result); // No need for Result<T> here as it's a plain retrieval
        }

        // GET: api/articles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            var result = await _articleService.GetArticleByIdAsync(id);

            // Handle the case when the article is not found
            if (result == null)
                return NotFound($"Article with id {id} not found.");

            return Ok(result); // The result is directly returned as Article object
        }

        // POST: api/articles
        [HttpPost]
        [Authorize(Roles = "Admin, Author")]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleDto articleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _articleService.CreateArticleAsync(articleDto);

            // Check the result of the creation process
            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return CreatedAtAction(nameof(GetArticleById), new { id = result.Data.Id }, result.Data);
        }

        // PUT: api/articles/{id}
        [Authorize(Roles = "Admin, Author")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] ArticleDto articleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _articleService.UpdateArticleAsync(id, articleDto);

            // Handle failure cases
            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return NoContent(); // Update successful
        }

        // DELETE: api/articles/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Author")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var result = await _articleService.DeleteArticleAsync(id);

            // Handle failure cases
            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return NoContent(); // Deletion successful
        }

    }
}
