using Cocorico.Server.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cocorico.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(ICustomAuthenticationService customAuthenticationService)
        {
            _customAuthenticationService = customAuthenticationService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginDetails credentials)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _customAuthenticationService.LoginAsync(credentials);
            //TODO: Handle fail

            return new JsonResult(result);
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterDetails model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _customAuthenticationService.RegisterAsync(model);
            //TODO: Handle fail

            return Ok();
        }

        [Authorize(Roles = Verbs.CocoricoUser)]
        [HttpPost(nameof(Logout))]
        public async Task<IActionResult> Logout()
        {
            await _customAuthenticationService.LogoutAsync();
            //TODO: Handle fail

            return Ok();
        }
    }
}
