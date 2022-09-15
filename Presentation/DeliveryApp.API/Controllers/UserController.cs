using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult>Create(CreateUser user)
        {
            CreateUserResponse response = await _userService.CreateAsync(user);

            return Ok(response);
        }
    }
}
