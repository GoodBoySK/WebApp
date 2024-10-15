using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Services;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(AppDbContext dbContext) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage([FromServices] IImageStorageService imageStorageService, Guid id)
        {
            try
            {
                var mediaFile = await dbContext.MediaFiles.FindAsync(id);

                if (mediaFile == null)
                {
                    return NotFound();
                }

                var image = await imageStorageService.RetriveImageAsync(mediaFile);
                return File(image, GetContentType(Path.GetExtension(mediaFile.Path)).TrimStart('.'));
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
        }

        
        private string GetContentType(string extension)
        {
            switch (extension)
            {
                case "jpg": return "image/jpeg";
                case "jpeg": return "image/jpeg";
                case "png": return "image/png";
                case "gif": return "image/gif";
                default: return "application/octet-stream";
            }
        }
    }
}
