using E_D_Project_1.Models;
using E_D_Project_1.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_D_Project_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TheController : ControllerBase
    {
        private readonly IEdService _edService;

        public TheController(IEdService edService)
        {
            _edService = edService;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Ed1 request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                var registeredUser = await _edService.RegisterUserAsync(request);
                return Ok("User registered successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "Registration failed.");
            }
        }




        // API
        // LOGIN API
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] Ed1 request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login data...");
            }

            try
            {
                var result = await _edService.LoginUserAsync(request);
                if (result == "Login successful")
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = result
                    });
                }
                else
                {
                    return Unauthorized(new
                    {
                        Success = false,
                        Message = result
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = $"Login failed: {ex.Message}"
                });
            }
        }
    }
}
