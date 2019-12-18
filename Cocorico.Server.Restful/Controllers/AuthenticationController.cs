using Cocorico.Application.Users.Queries.GetClaims;
using Cocorico.Domain.Identity;
using Cocorico.Server.Domain.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cocorico.Server.Restful.Controllers
{
    [Produces(Verbs.ApplicationJson)]
    [Route(Verbs.ApiController)]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServerCocoricoAuthenticationService _serverCocoricoAuthenticationService;
        private readonly IMediator _mediator;

        public AuthenticationController(
            IServerCocoricoAuthenticationService serverCocoricoAuthenticationService,
            IMediator mediator)
        {
            _serverCocoricoAuthenticationService = serverCocoricoAuthenticationService;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost(nameof(LoginAsync))]
        public async Task<ActionResult<ClaimsDto>> LoginAsync([FromBody] LoginDetails credentials)
        {
            var result = await _serverCocoricoAuthenticationService.LoginAsync(credentials);

            return new ActionResult<ClaimsDto>(result);
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

        [Authorize(Policy = Policies.User)]
        [HttpGet(nameof(GetCurrentUserClaims))]
        public async Task<ActionResult<ClaimsDto>> GetCurrentUserClaims()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var claims = await _mediator.Send(new GetUserClaimsQuery(new UserIdDto { UserId = userId }));

            return new ActionResult<ClaimsDto>(claims);
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
