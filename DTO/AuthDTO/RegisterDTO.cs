
using Enums;

namespace DTO.AuthDTO
{
    public class RegisterDTO
    {
        public required string Email {  get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public UserSexeEnum? Sexe { get; set; }
        public string? ImageURL { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
