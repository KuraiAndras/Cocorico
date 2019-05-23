using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Services.Sandwich;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<SandwichDto>>> GetAllAsync()
        {
            var serviceResult = await _serverSandwichService.GetAllSandwichResultAsync();

            return new ActionResult<IEnumerable<SandwichDto>>(serviceResult);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SandwichDto>> GetAsync([FromRoute] int id)
        {
            var serviceResult = await _serverSandwichService.GetSandwichResultAsync(id);

            return new ActionResult<SandwichDto>(serviceResult);
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] SandwichDto sandwich)
        {
            await _serverSandwichService.UpdateSandwichAsync(sandwich);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] SandwichAddDto sandwich)
        {
            await _serverSandwichService.AddSandwichAsync(sandwich);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _serverSandwichService.DeleteSandwichAsync(id);

            return new OkResult();
        }
    }
}
