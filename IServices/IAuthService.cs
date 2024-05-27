
using DTO.AuthDTO;

namespace IServices
{
    public interface IAuthService
    {
        public Task RegisterAsync(RegisterDTO registerServiceDTO);
    }
}
