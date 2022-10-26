using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.DTOs.User;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.CourierMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICourierService _courierService;
        private readonly IAuthService _authService;
        public AccountController(SignInManager<AppUser> signInManager, IUserService userService, IAuthService authService, ICourierService courierService)
        {
            _signInManager = signInManager;
            _authService = authService;
            _courierService = courierService;
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

                return RedirectToAction("index", "home");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
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
                CreateUserResponse response = await _courierService.CreateAsync(createUser);

                return RedirectToAction("login");
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
