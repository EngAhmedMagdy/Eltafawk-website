using EltafawkPlatform.Models;
using EltafawkPlatform.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EltafawkPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authService.RegisterAsync(model.Email, model.Password, model.FullName);
            if (result.Succeeded)
                return Ok(new ApiResponse { Success = true, Message = "User registered successfully." });

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authService.LoginAsync(model.Email, model.Password);
            if (result.Succeeded)
                return Ok(new ApiResponse { Success = true, Message = "Logged in successfully." });

            return Unauthorized(new ApiResponse { Success = false, Message = "Invalid credentials." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok(new ApiResponse{ Success = true, Message = "Logged out successfully." });
        }
    }
}
