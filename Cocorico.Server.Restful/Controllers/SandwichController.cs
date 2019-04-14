using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Services.Sandwich;
using Cocorico.Server.Restful.Extensions;
using Cocorico.Shared.Dtos.Sandwich;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cocorico.Shared.Helpers;

namespace Cocorico.Server.Restful.Controllers
{
    [Produces(Verbs.ApplicationJson)]
    [Route(Verbs.ApiController)]
    [ApiController]
    public class SandwichController : ControllerBase
    {
        private readonly IServerSandwichService _serverSandwichService;

        public SandwichController(IServerSandwichService serverSandwichService) => _serverSandwichService = serverSandwichService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var serviceResult = await _serverSandwichService.GetAllSandwichResultAsync();

            return serviceResult.ToActionResult();
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var serviceResult = await _serverSandwichService.GetSandwichResultAsync(id);

            return serviceResult.ToActionResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateAsync([FromBody] NewSandwichDto sandwich)
        {
            var serviceResult = await _serverSandwichService.AddOrUpdateSandwichAsync(sandwich);

            return serviceResult.ToActionResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var serviceResult = await _serverSandwichService.DeleteSandwichAsync(id);

            return serviceResult.ToActionResult();
        }
    }
}
