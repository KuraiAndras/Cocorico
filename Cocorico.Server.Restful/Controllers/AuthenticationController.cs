using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Services.Authentication;
using Cocorico.Server.Restful.Extensions;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cocorico.Server.Restful.Controllers
{
    [Produces(Verbs.ApplicationJson)]
    [Route(Verbs.ApiController)]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServerCocoricoAuthenticationService _serverCocoricoAuthenticationService;

        public AuthenticationController(IServerCocoricoAuthenticationService serverCocoricoAuthenticationService) => _serverCocoricoAuthenticationService = serverCocoricoAuthenticationService;

        [AllowAnonymous]
        [HttpPost(Urls.ServerAction.Login)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDetails credentials)
        {
            var result = await _serverCocoricoAuthenticationService.LoginAsync(credentials);

            return result.ToActionResult();
        }

        [AllowAnonymous]
        [HttpPost(Urls.ServerAction.Register)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDetails model)
        {
            var result = await _serverCocoricoAuthenticationService.RegisterAsync(model);

            return result.ToActionResult();
        }

        [Authorize(Policy = Policies.User)]
        [HttpPost(Urls.ServerAction.Logout)]
        public async Task<IActionResult> LogoutAsync()
        {
            var result = await _serverCocoricoAuthenticationService.LogoutAsync();

            return result.ToActionResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost(Urls.ServerAction.AddClaimToUser)]
        public async Task<IActionResult> AddClaimToUserAsync([FromBody] UserClaimRequest userClaimRequest)
        {
            var result = await _serverCocoricoAuthenticationService.AddClaimToUserAsync(userClaimRequest);

            return result.ToActionResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost(Urls.ServerAction.RemoveClaimFromUser)]
        public async Task<IActionResult> RemoveClaimFromUserAsync([FromBody] UserClaimRequest userClaimRequest)
        {
            var result = await _serverCocoricoAuthenticationService.RemoveClaimFromUserAsync(userClaimRequest);

            return result.ToActionResult();
        }
    }
}
