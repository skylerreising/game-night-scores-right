using Microsoft.AspNetCore.Mvc;
using GameNightScoresRight.ControllerDTOs;
using GameNightScoresRight.Managers;

namespace GameNightScoresRight.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserManager userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var response = await _userManager.CreateUser(request);
            return response != null ? Ok(response) : BadRequest("Failed to create user.");
        }
    }
}
