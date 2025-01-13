using server.Dtos;
using server.Models;

namespace server.Interfaces
{
    public interface IBlogPostService
    {
        Task<BlogPost> CreateBlogPostAsync(CreateBlogPostDto blogPost, User user);
        Task<(ICollection<BlogPost>, int)> GetAllBlogsFilterAsync(Filter? filter);
        Task<BlogPost?> GetBlogPostByIdAsync(int blogPostId);
    }
}
