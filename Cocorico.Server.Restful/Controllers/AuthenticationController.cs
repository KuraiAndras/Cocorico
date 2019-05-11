using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Services.Authentication;
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
        public async Task<ActionResult<LoginResult>> LoginAsync([FromBody] LoginDetails credentials)
        {
            var result = await _serverCocoricoAuthenticationService.LoginAsync(credentials);

            return new ActionResult<LoginResult>(result);
        }

        [AllowAnonymous]
        [HttpPost(Urls.ServerAction.Register)]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterDetails model)
        {
            await _serverCocoricoAuthenticationService.RegisterAsync(model);

            return new OkResult();
        }

        [Authorize(Policy = Policies.User)]
        [HttpPost(Urls.ServerAction.Logout)]
        public async Task<ActionResult> LogoutAsync()
        {
            await _serverCocoricoAuthenticationService.LogoutAsync();

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost(Urls.ServerAction.AddClaimToUser)]
        public async Task<ActionResult> AddClaimToUserAsync([FromBody] UserClaimRequest userClaimRequest)
        {
            await _serverCocoricoAuthenticationService.AddClaimToUserAsync(userClaimRequest);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost(Urls.ServerAction.RemoveClaimFromUser)]
        public async Task<ActionResult> RemoveClaimFromUserAsync([FromBody] UserClaimRequest userClaimRequest)
        {
            await _serverCocoricoAuthenticationService.RemoveClaimFromUserAsync(userClaimRequest);

            return new OkResult();
        }
    }
}
