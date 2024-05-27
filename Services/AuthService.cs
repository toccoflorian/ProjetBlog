
using DTO.AuthDTO;
using Enums;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthRepository _authRepository;
        public AuthService(UserManager<AppUser> userManager, IAuthRepository authRepository)
        {
            this._userManager = userManager;
            this._authRepository = authRepository;
        }

        public async Task RegisterAsync(RegisterDTO registerServiceDTO)
        {
            if (registerServiceDTO.Password != registerServiceDTO.ConfirmPassword)
                throw new Exception("Le mot de passe et la confirmation du mot de passe ne sont pas identiques.");
            AppUser appuser = new AppUser
            {
                Email = registerServiceDTO.Email,
                NormalizedEmail = registerServiceDTO.Email.ToUpper(),
                UserName = registerServiceDTO.Email.ToUpper(),
                NormalizedUserName = registerServiceDTO.Email.ToUpper(),
            };
            IdentityResult? result = await this._userManager.CreateAsync(appuser, registerServiceDTO.Password);
            if (result.Succeeded)
            {
                try
                {
                    await this._authRepository.CreateAsync(new User
                    {
                        Id = appuser.Id,
                        Firstname = registerServiceDTO.Firstname,
                        Lastname = registerServiceDTO.Lastname,
                        Sexe = registerServiceDTO.Sexe != null ? 
                            (UserSexeEnum)registerServiceDTO.Sexe 
                            : 
                            UserSexeEnum.NC,
                        ImageUrl = registerServiceDTO.ImageURL != null ? 
                            registerServiceDTO.ImageURL 
                            : 
                            CONST.USER.DefaultUserImageURL,
                        SignInDate = DateTime.Now,
                        AppUserId = appuser.Id,
                    });
                    return;
                }
                catch
                {
                    await this._userManager.DeleteAsync(appuser);
                    throw new Exception("Une erreur est survenue lors de l'inscription, merci de contacter le support.");
                }
            }
            throw new Exception("Une erreur est survenue lors de l'inscription, merci de contacter le support.");
        }
    }
}
