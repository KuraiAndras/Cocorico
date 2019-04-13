using Cocorico.Server.Extensions;
using Cocorico.Server.Helpers;
using Cocorico.Server.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cocorico.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService) => _authenticationService = authenticationService;

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginDetails credentials)
        {
            var result = await _authenticationService.LoginAsync(credentials);

            return result.ToActionResult();
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterDetails model)
        {
            var result = await _authenticationService.RegisterAsync(model);

            return result.ToActionResult();
        }

        [Authorize(Policy = Policies.User)]
        [HttpPost(nameof(Logout))]
        public async Task<IActionResult> Logout()
        {
            var result = await _authenticationService.LogoutAsync();

            return result.ToActionResult();
        }
    }
}
