using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Services.IngredientService;
using Cocorico.Shared.Dtos.Ingredient;
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
    public class IngredientController : ControllerBase
    {
        private readonly IServerIngredientService _serverIngredientService;

        public IngredientController(IServerIngredientService serverIngredientService)
        {
            _serverIngredientService = serverIngredientService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetAllAsync() =>
            new ActionResult<IEnumerable<IngredientDto>>(await _serverIngredientService.GetAllAsync());

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IngredientDto>> GetAsync([FromRoute] int id) =>
            new ActionResult<IngredientDto>(await _serverIngredientService.GetAsync(id));

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] IngredientAddDto ingredientAddDto)
        {
            await _serverIngredientService.AddAsync(ingredientAddDto);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] IngredientDto ingredientDto)
        {
            await _serverIngredientService.UpdateAsync(ingredientDto);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _serverIngredientService.DeleteAsync(id);

            return new OkResult();
        }
    }
}
