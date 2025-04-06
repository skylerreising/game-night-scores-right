using GameNightScoresRight.ControllerDTOs;
using GameNightScoresRight.Managers;
using Microsoft.AspNetCore.Mvc;

namespace GameNightScoresRight.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            var response = await _accountManager.CreateAccount(request);
            return response != null ? Ok(response) : BadRequest();
        }
    }
}
