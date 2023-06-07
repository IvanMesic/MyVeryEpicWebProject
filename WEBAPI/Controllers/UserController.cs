using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octopus.Client.Repositories.Async;
using WEBAPI.Models;
using WEBAPI.Services;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepository;

        public UserController(IUserRepo userRepo)
        {
            _userRepository = userRepo;
        }

        [HttpPost("[action]")]
        public ActionResult<User> Register([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newUser = _userRepository.Add(request);

                return Ok(new UserRegisterResponse
                {
                    Id = newUser.Id,
                    SecurityToken = newUser.SecurityToken
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult ValidateEmail([FromBody] ValidateEmailRequest request)
        {
            try
            {
                _userRepository.ValidateEmail(request);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult<Tokens> JwtTokens([FromBody] JWTokenRequests request)
        {
            try
            {
                return Ok(_userRepository.JwtTokens(request));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                _userRepository.ChangePassword(request);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}