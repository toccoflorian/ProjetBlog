using DTO.CommentDTO;

namespace IServices
{
    public interface ICommentService
    {
        public Task WriteComment(WriteCommentServiceDTO writeCommentServiceDTO);
        public Task<List<GetCommentResponseDTO>> GetAllAsync();
    }
}
