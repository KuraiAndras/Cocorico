using Cocorico.Application.Users.Commands.AddClaimToUser;
using Cocorico.Application.Users.Commands.LoginUser;
using Cocorico.Application.Users.Commands.LogoutUser;
using Cocorico.Application.Users.Commands.RegisterUser;
using Cocorico.Application.Users.Commands.RemoveClaimFromUser;
using Cocorico.Application.Users.Queries.GetClaims;
using Cocorico.Shared.Dtos.Authentication;
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
        public async Task<ActionResult<ClaimsDto>> LoginAsync([FromBody] LoginDetails credentials)
        {
            await _mediator.Send(new LoginUserCommand(credentials));

            var result = await _mediator.Send(new GetUserClaimsByNameQuery(new UserNameDto { UserName = credentials.Email }));

            return new ActionResult<ClaimsDto>(result);
        }

        [AllowAnonymous]
        [HttpPost(nameof(RegisterAsync))]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterDetails model)
        {
            await _mediator.Send(new RegisterUserCommand(model));

            return new OkResult();
        }

        [Authorize(Policy = Policies.User)]
        [HttpPost(nameof(LogoutAsync))]
        public async Task<ActionResult> LogoutAsync()
        {
            await _mediator.Send(new LogoutCurrentUserCommand());

            return new OkResult();
        }

        [Authorize(Policy = Policies.User)]
        [HttpGet(nameof(GetCurrentUserClaimsAsync))]
        public async Task<ActionResult<ClaimsDto>> GetCurrentUserClaimsAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var claims = await _mediator.Send(new GetUserClaimsQuery(new UserIdDto { UserId = userId }));

            return new ActionResult<ClaimsDto>(claims);
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost(nameof(AddClaimToUserAsync))]
        public async Task<ActionResult> AddClaimToUserAsync([FromBody] UserClaimRequest userClaimRequest)
        {
            await _mediator.Send(new AddClaimToUserCommand(userClaimRequest));

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost(nameof(RemoveClaimFromUserAsync))]
        public async Task<ActionResult> RemoveClaimFromUserAsync([FromBody] UserClaimRequest userClaimRequest)
        {
            await _mediator.Send(new RemoveClaimFromUserCommand(userClaimRequest));

            return new OkResult();
        }
    }
}
