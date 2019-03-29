using Cocorico.Server.Services.Sandwich;
using Cocorico.Shared.Dtos.Sandwich;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cocorico.Server.Controllers
{
    //TODO: Authorize
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SandwichController : ControllerBase
    {
        private readonly ISandwichService _sandwichService;

        public SandwichController(ISandwichService sandwichService) => _sandwichService = sandwichService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return new JsonResult(await _sandwichService.GetAllAsync());
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync([FromRoute] int key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return new JsonResult(await _sandwichService.GetAsync(key));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateAsync([FromForm] NewSandwichDto sandwich)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _sandwichService.AddOrUpdateAsync(sandwich);

            return new OkResult();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _sandwichService.DeleteAsync(key);

            return new OkResult();
        }
    }
}
