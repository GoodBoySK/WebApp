using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos;
using server.Interfaces;
using server.Models;

namespace server.Services
{
    public class BlogPostService(AppDbContext dbContext) : IBlogPostService
    {
        public async Task<BlogPost?> GetBlogPostByIdAsync(int blogPostId)
        {
            return await dbContext.Blogs
                .Include(b => b.Thumbnail)
                .Include(b => b.Tag)
                .Include(b => b.Autor)
                .FirstOrDefaultAsync(b => b.Id == blogPostId);
        }

        public async Task<BlogPost> CreateBlogPostAsync(CreateBlogPostDto blogPost, User user)
        {
            Tag? tag = null;

            if (blogPost.Tag is not null)
            {
                tag = new Tag { Name = blogPost.Tag };
                
            }

            MediaFile? mediaFile = dbContext.MediaFiles.FirstOrDefault(mf => mf.Id == blogPost.MediaFileID);

            var newBlogPost = new BlogPost
            {
                Autor = user,
                Tag = tag,
                Title = blogPost.Title,
                Content = blogPost.Content,
                CreatedAt = DateTime.Now,
                Desctiption = blogPost.Description,
                Thumbnail = mediaFile
            };
            var result = await dbContext.Blogs.AddAsync(newBlogPost);
            if(tag is not null)
                await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<(ICollection<BlogPost>, int)> GetAllBlogsFilterAsync(Filter? filter)
        {
            IQueryable<BlogPost> list = dbContext.Blogs
                .IgnoreQueryFilters()
                .Include(x => x.Autor)
                .Include(x => x.Thumbnail)
                .Include(x => x.Tag)
                ;

            if (filter is null)
            {
                return (await list.ToListAsync(), await list.CountAsync());
            }

            if (filter.NameFilter is not null)
            {
                list = list.Where(x => x.Title.Contains(filter.NameFilter));
            }

            if (filter.TagFilter is not null)
            {
                list = list.Where(x => x.Tag != null && x.Tag.Name.Contains(filter.TagFilter));
            }

            if (filter.Order is not null)
            {
                switch (filter.Order)
                {
                    case OrderBy.Name:
                        list = filter.Ascending ? list.OrderBy(x => x.Title) : list.OrderByDescending(x => x.Title);
                        break;
                    case OrderBy.CreatedData:
                        list = filter.Ascending ? list.OrderBy(x => x.CreatedAt) : list.OrderByDescending(x => x.CreatedAt);
                        break;
                }
            }

            return (await list.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync(), await list.CountAsync());
        }
    }
}
