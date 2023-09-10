using Common.DALModels;
using WEBAPI.Models;

namespace WEBAPI.Services
{
    public interface IUserRepoAPI
    {
        User Add(UserRegisterRequest request);
        void ValidateEmail(ValidateEmailRequest request);
        Tokens JwtTokens(JWTokenRequests request);
        void ChangePassword(ChangePasswordRequest request);
    }
}
