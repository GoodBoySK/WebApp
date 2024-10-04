using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Data;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController(AppDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var a = dbContext.Recipes.First();
            return Ok("This is the response from the RecipeController");
        }
    }
}
