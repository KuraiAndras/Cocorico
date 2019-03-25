using System.Threading.Tasks;
using Cocorico.RazorComponents.Models.Entities.Sandwich;
using Cocorico.RazorComponents.Services.Sandwich;
using Microsoft.AspNetCore.Mvc;

namespace Cocorico.RazorComponents.Controllers
{
    //TODO: Add Authentication and Authorization
    [Route("api/[controller]")]
    public class SandwichController : Controller
    {
        private readonly ISandwichService _sandwichService;

        public SandwichController(ISandwichService sandwichService)
        {
            _sandwichService = sandwichService;
        }

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

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateAsync([FromForm] NewSandwichDto sandwich)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _sandwichService.AddOrUpdateAsync(sandwich);

            return new OkResult();
        }

        //[Authorize]
        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _sandwichService.DeleteAsync(key);

            return new OkResult();
        }
    }
}
