using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Dtos;
using server.Interfaces;
using server.Models;

namespace server.Services
{
    public class CommentService(AppDbContext dbContext) : ICommentService
    {
        public async Task<Comment> CreateCommentAsync(Recipe recipe, User user, AddCommentDto commentDto)
        {
            var comment = new Comment
            {
                Text = commentDto.Text,
                CreatedAt = DateTime.Now,
                Parent = null,
                UpdatedAt = DateTime.Now,
                CreatedBy = user
            };
            
            await dbContext.Comments.AddAsync(comment);

            recipe.Comments.Add(comment); 
            
            await dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteComment(Guid commentId, User user)
        {
            var comment = await dbContext.Comments.FindAsync(commentId);

            if (comment == null) return false;

            if (comment.CreatedBy.Id != user.Id) return false;

            dbContext.Comments.Remove(comment);

            await dbContext.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> UpdateCommnet(Guid commentId, UpdateCommentDto updateCommentDto, User user)
        {
            var comment = await dbContext.Comments.FindAsync(commentId);
            if (comment == null) return false;

            if (comment.CreatedBy.Id != user.Id) return false;

            comment.Text = updateCommentDto.Text;
            comment.UpdatedAt = DateTime.Now;
            
            await dbContext.SaveChangesAsync();
            
            return true;
        }
    }
}
