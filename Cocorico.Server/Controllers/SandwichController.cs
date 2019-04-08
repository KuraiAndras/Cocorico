using Cocorico.Server.Services.Sandwich;
using Cocorico.Shared.Dtos.Sandwich;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Cocorico.Server.Controllers
{
    //TODO: Authorize
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

            return new JsonResult(await _sandwichService.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet("{key}")]
        public async Task<IActionResult> Get([FromRoute] int key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return new JsonResult(await _sandwichService.GetAsync(key));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromBody] NewSandwichDto sandwich)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _sandwichService.AddOrUpdateAsync(sandwich);

            return new OkResult();
        }

        [Authorize]
        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _sandwichService.DeleteAsync(key);

            return new OkResult();
        }
    }
}
