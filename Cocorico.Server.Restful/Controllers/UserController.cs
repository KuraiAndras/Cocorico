using Cocorico.Server.Domain.Services.User;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Restful.Controllers
{
    [Produces(Verbs.ApplicationJson)]
    [Route(Verbs.ApiController)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServerUserService _userService;

        public UserController(IServerUserService userService) => _userService = userService;

        [Authorize(Policy = Policies.Administrator)]
        [HttpGet(nameof(GetAllForAdminAsync))]
        public async Task<ActionResult<IEnumerable<UserForAdminPage>>> GetAllForAdminAsync()
        {
            var serviceResult = await _userService.GetAllUsersForAdminPageAsync();

            return new ActionResult<IEnumerable<UserForAdminPage>>(serviceResult);
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpGet(nameof(GetUserForAdminPageAsync) + "/{userId}")]
        public async Task<ActionResult<UserForAdminPage>> GetUserForAdminPageAsync([FromRoute] string userId)
        {
            var serviceResult = await _userService.GetUserForAdminPageAsync(userId);

            return new ActionResult<UserForAdminPage>(serviceResult);
        }
    }
}
