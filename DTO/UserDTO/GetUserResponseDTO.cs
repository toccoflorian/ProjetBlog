using Enums;
using Models;

namespace DTO.UserDTO
{
    public class GetUserResponseDTO
    {
        public string Id { get; set; }
        public string Firstsname { get; set; }
        public string Lastsname { get; set; }
        public string ImageURL { get; set; }
        public UserSexeEnum Sexe { get; set; }
        public DateTime SignInDate { get; set; }

        public GetUserResponseDTO(User user) 
        {
            this.Id = user.Id;
            this.Firstsname = user.Firstname;
            this.Lastsname = user.Lastname;
            this.ImageURL = user.ImageUrl;
            this.Sexe = user.Sexe; // il faut gerer le sexe avec un enum ou autre
            this.SignInDate = user.SignInDate;
        }
    }
}
