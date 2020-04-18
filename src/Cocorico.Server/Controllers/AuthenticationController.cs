using Cocorico.Shared.Api.Authentication;
using Cocorico.Shared.Api.Users;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cocorico.Server.Controllers
{
    [Produces(Verbs.ApplicationJson)]
    [Route(Verbs.ApiController)]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator) => _mediator = mediator;

        [AllowAnonymous]
        [HttpPost(nameof(LoginAsync))]
        public async Task<ActionResult<ClaimsDto>> LoginAsync([FromBody] LoginUser credentials)
        {
            // TODO: Return claims
            await _mediator.Send(credentials);

            var result = await _mediator.Send(new GetUserClaimsByName { UserName = credentials.Email });

            return new ActionResult<ClaimsDto>(result);
        }

        [AllowAnonymous]
        [HttpPost(nameof(RegisterAsync))]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterUser model)
        {
            await _mediator.Send(model);

            return new OkResult();
        }

        [Authorize(Policy = Policies.User)]
        [HttpPost(nameof(LogoutAsync))]
        public async Task<ActionResult> LogoutAsync()
        {
            await _mediator.Send(new LogoutCurrentUser());

            return new OkResult();
        }

        [Authorize(Policy = Policies.User)]
        [HttpGet(nameof(GetCurrentUserClaimsAsync))]
        public async Task<ActionResult<ClaimsDto>> GetCurrentUserClaimsAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var claims = await _mediator.Send(new GetUserClaims { UserId = userId });

            return new ActionResult<ClaimsDto>(claims);
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost(nameof(AddClaimToUserAsync))]
        public async Task<ActionResult> AddClaimToUserAsync([FromBody] AddClaimToUser addClaimToUser)
        {
            await _mediator.Send(addClaimToUser);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost(nameof(RemoveClaimFromUserAsync))]
        public async Task<ActionResult> RemoveClaimFromUserAsync([FromBody] RemoveClaimFromUser userClaimRequest)
        {
            await _mediator.Send(userClaimRequest);

            return new OkResult();
        }
    }
}
