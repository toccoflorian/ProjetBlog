
using DTO.CommentDTO;
using IRepositories;
using IServices;
using Models;

namespace Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(
            ICommentRepository commentRepository)
        {
            this._commentRepository = commentRepository;
        }

        public async Task WriteComment(WriteCommentServiceDTO commentDTO)
        {
            await this._commentRepository.CreateAsync(new Comment
            {
                ArticleId = commentDTO.ArticleId,
                UserId = commentDTO.UserId,
                Content = commentDTO.Content,
                EditionDate = DateTime.Now,
            });
            await this._commentRepository.SaveChangesAsync();
        }

        public async Task<List<GetCommentResponseDTO>> GetAllAsync()
        {
            List<GetCommentResponseDTO> comments = new List<GetCommentResponseDTO>();
            foreach (Comment comment in await this._commentRepository.getAllAsync()) 
            {
                comments.Add(new GetCommentResponseDTO(comment));
            }
            return comments;
        }
    }
}
