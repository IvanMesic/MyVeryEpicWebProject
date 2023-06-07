using WEBAPI.Models;

namespace WEBAPI.Services
{
    public interface IUserRepo
    {
        User Add(UserRegisterRequest request);
        void ValidateEmail(ValidateEmailRequest request);
        Tokens JwtTokens(JWTokenRequests request);
        void ChangePassword(ChangePasswordRequest request);
    }
}
