using Cocorico.Shared.Api.Users;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Controllers
{
    [Produces(Verbs.ApplicationJson)]
    [Route(Verbs.ApiController)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) => _mediator = mediator;

        [Authorize(Policy = Policies.Administrator)]
        [HttpGet(nameof(GetAllForAdminAsync))]
        public async Task<ActionResult<IEnumerable<UserForAdminPage>>> GetAllForAdminAsync()
        {
            var serviceResult = await _mediator.Send(new GetAllUsersForAdmin());

            return new ActionResult<IEnumerable<UserForAdminPage>>(serviceResult);
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpGet(nameof(GetUserForAdminPageAsync) + "/{userId}")]
        public async Task<ActionResult<UserForAdminPage>> GetUserForAdminPageAsync([FromRoute] string userId)
        {
            var serviceResult = await _mediator.Send(new GetUserForAdminQuery { UserId = userId });

            return new ActionResult<UserForAdminPage>(serviceResult);
        }
    }
}
