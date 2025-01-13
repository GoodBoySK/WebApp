using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Interfaces;
using server.Models;
using System.Security.Claims;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController(IBlogPostService blogPostService, UserManager<User> userManager) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var blogPost = await blogPostService.GetBlogPostByIdAsync(id);

            return Ok(blogPost);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CreateBlogPostDto blogPostDto)
        {
            string? id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (id is null)
            {
                return Unauthorized();
            }

            User? user = userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user is null)
            {
                return Unauthorized();
            }

            var blogPost = await blogPostService.CreateBlogPostAsync(blogPostDto, user);
        
            return blogPost is null ? StatusCode(500) : Ok(blogPost);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAllWithFilter([FromBody] Filter? filter)
        {
            var (blogs, count) = await blogPostService.GetAllBlogsFilterAsync(filter);
            return Ok(new {count, blogs });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BlogPost blogPost)
        {
            return StatusCode(501);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return StatusCode(501);
        }
    }
}
