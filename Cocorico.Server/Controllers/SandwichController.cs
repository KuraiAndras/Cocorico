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
    public class SandwichController : ControllerBase
    {
        private readonly ISandwichService _sandwichService;

        public SandwichController(ISandwichService sandwichService) => _sandwichService = sandwichService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var serviceResult = await _sandwichService.GetAllAsync();

            return serviceResult.ToActionResult();
        }

        [AllowAnonymous]
        [HttpGet("{key}")]
        public async Task<IActionResult> Get([FromRoute] int key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var serviceResult = await _sandwichService.GetAsync(key);

            return serviceResult.ToActionResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromBody] NewSandwichDto sandwich)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var serviceResult = await _sandwichService.AddOrUpdateAsync(sandwich);

            return serviceResult.ToActionResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var serviceResult = await _sandwichService.DeleteAsync(key);

            return serviceResult.ToActionResult();
        }
    }
}
