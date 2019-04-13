using Cocorico.Server.Services.Sandwich;
using Cocorico.Shared.Dtos.Sandwich;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cocorico.Server.Extensions;
using Cocorico.Server.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Cocorico.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SandwichController : ControllerBase
    {
        private readonly ISandwichService _sandwichService;

        public SandwichController(ISandwichService sandwichService) => _sandwichService = sandwichService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var serviceResult = await _sandwichService.GetAllSandwichResultAsync();

            return serviceResult.ToActionResult();
        }

        [AllowAnonymous]
        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync([FromRoute] int key)
        {
            var serviceResult = await _sandwichService.GetSandwichResult(key);

            return serviceResult.ToActionResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateAsync([FromBody] NewSandwichDto sandwich)
        {
            var serviceResult = await _sandwichService.AddOrUpdateSandwichAsync(sandwich);

            return serviceResult.ToActionResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int key)
        {
            var serviceResult = await _sandwichService.DeleteSandwichAsync(key);

            return serviceResult.ToActionResult();
        }
    }
}
