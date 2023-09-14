using AutoMapper;
using Common.BLModels;
using Common.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyEpicMVCProj.Models;
using MyEpicMVCProj.ViewModels;
using System.Security.Claims;

namespace MyEpicMVCProj.Controllers
{
    public class UserController : Controller
    {
        private static List<VMUser> _persons = new List<VMUser>();
        private static IUserReposi _userRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserReposi userRepository, IMapper mapper)
        {
            _userRepo = userRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var blUsers = _userRepo.GetAll();
            var vmUsers = _mapper.Map<IEnumerable<VMUser>>(blUsers);

            return View(vmUsers);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(VMRegister register)
        {
            if (!ModelState.IsValid)
                return View(register);

            var user = _userRepo.CreateUser(
                register.Username,
                register.FirstName,
                register.LastName,
                register.Email,
                register.Password,
                register.Country
                );

            return RedirectToAction("Index", "VideoMVC");
        }

        public IActionResult ValidateEmail(VMValidateEmail validateEmail)
        {
            if (!ModelState.IsValid)
                return View(validateEmail);

            // Confirm email, skip BL for simplicity
            _userRepo.ConfirmEmail(
                validateEmail.Email,
                validateEmail.SecurityToken);

            return RedirectToAction("Index");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(VMLogin login)
        {
            if (!ModelState.IsValid)
                return View(login);

            var user = _userRepo.GetConfirmedUser(
                login.Username,
                login.Password);

            if (user == null)
            {
                ModelState.AddModelError("Username", "Invalid username or password");
                return View(login);
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties()).Wait();

            return RedirectToAction("Index", "VideoMVC");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return RedirectToAction("Index", "VideoMVC");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(VMChangePassword changePassword)
        {
            // Change user password, skip BL for simplicity
            _userRepo.ChangePassword(
                changePassword.Username,
                changePassword.NewPassword);

            return RedirectToAction("Index");
        }
    }


}

