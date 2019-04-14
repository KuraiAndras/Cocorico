using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Services.User;
using Cocorico.Server.Restful.Extensions;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet(Urls.ServerAction.GetAllUsersForAdminPage)]
        public async Task<IActionResult> GetAllForAdminAsync()
        {
            var result = await _userService.GetAllUsersForAdminPageAsync();

            return result.ToActionResult();
        }
    }
}
