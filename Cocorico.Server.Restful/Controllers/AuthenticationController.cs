using Cocorico.Domain.Identity;
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
        [HttpPost(nameof(LoginAsync))]
        public async Task<ActionResult<LoginResult>> LoginAsync([FromBody] LoginDetails credentials)
        {
            var result = await _serverCocoricoAuthenticationService.LoginAsync(credentials);

            return new ActionResult<LoginResult>(result);
        }

        [AllowAnonymous]
        [HttpPost(nameof(RegisterAsync))]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterDetails model)
        {
            await _serverCocoricoAuthenticationService.RegisterAsync(model);

            return new OkResult();
        }

        [Authorize(Policy = Policies.User)]
        [HttpPost(nameof(LogoutAsync))]
        public async Task<ActionResult> LogoutAsync()
        {
            await _serverCocoricoAuthenticationService.LogoutAsync();

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost(nameof(AddClaimToUserAsync))]
        public async Task<ActionResult> AddClaimToUserAsync([FromBody] UserClaimRequest userClaimRequest)
        {
            await _serverCocoricoAuthenticationService.AddClaimToUserAsync(userClaimRequest);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost(nameof(RemoveClaimFromUserAsync))]
        public async Task<ActionResult> RemoveClaimFromUserAsync([FromBody] UserClaimRequest userClaimRequest)
        {
            await _serverCocoricoAuthenticationService.RemoveClaimFromUserAsync(userClaimRequest);

            return new OkResult();
        }
    }
}
