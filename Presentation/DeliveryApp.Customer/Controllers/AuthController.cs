using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.DTOs.User;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace DeliveryApp.Customer.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager, IUserService userService, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userService = userService;
            _authService = authService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUser createUser)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                CreateUserResponse response = await _userService.CreateAsync(createUser);

                return RedirectToAction("login");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                await _authService.LoginAsync(loginDto);

                //if (result.IsLockedOut)
                //{
                //    ModelState.AddModelError("", "Email veya password invalid");
                //    return View(loginDto);
                //}
                //if (!result.Succeeded)
                //{
                //    ModelState.AddModelError("", "Email veya password invalid");
                //    return View(loginDto);
                //}
                //if (ReturnUrl != null) Redirect(ReturnUrl);

                return RedirectToAction("index", "home");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
