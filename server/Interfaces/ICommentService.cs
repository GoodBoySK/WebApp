using server.Dtos;
using server.Models;

namespace server.Interfaces
{
    public interface ICommentService
    {
        Task<bool> IsOwner(Guid commentId, User user);
        Task<Comment> CreateCommentAsync(Recipe recipe, User user, AddCommentDto commentDto);
        Task<bool> DeleteComment(Guid commentId, User user);
        Task<bool> UpdateCommnet(Guid commentId, UpdateCommentDto updateCommentDto, User user);
    }
}
