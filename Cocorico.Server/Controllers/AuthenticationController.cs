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
        private readonly IServerCocoricoAuthenticationService _serverCocoricoAuthenticationService;

        public AuthenticationController(IServerCocoricoAuthenticationService serverCocoricoAuthenticationService) => _serverCocoricoAuthenticationService = serverCocoricoAuthenticationService;

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginDetails credentials)
        {
            var result = await _serverCocoricoAuthenticationService.LoginAsync(credentials);

            return result.ToActionResult();
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterDetails model)
        {
            var result = await _serverCocoricoAuthenticationService.RegisterAsync(model);

            return result.ToActionResult();
        }

        [Authorize(Policy = Policies.User)]
        [HttpPost(nameof(Logout))]
        public async Task<IActionResult> Logout()
        {
            var result = await _serverCocoricoAuthenticationService.LogoutAsync();

            return result.ToActionResult();
        }
    }
}
