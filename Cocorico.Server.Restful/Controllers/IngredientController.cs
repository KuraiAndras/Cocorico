using Cocorico.Application.Ingredients.Commands.AddIngredient;
using Cocorico.Application.Ingredients.Commands.DeleteIngredient;
using Cocorico.Application.Ingredients.Commands.UpdateIngredient;
using Cocorico.Application.Ingredients.Queries.GetAllIngredients;
using Cocorico.Application.Ingredients.Queries.GetIngredient;
using Cocorico.Domain.Identity;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Helpers;
using MediatR;
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
        private readonly IMediator _mediator;

        public IngredientController(IMediator mediator) => _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetAllAsync() =>
            new ActionResult<IEnumerable<IngredientDto>>(await _mediator.Send(new GetAllIngredientsQuery()));

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IngredientDto>> GetAsync([FromRoute] int id) =>
            new ActionResult<IngredientDto>(await _mediator.Send(new GetIngredientQuery(id)));

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] IngredientAddDto ingredientAddDto)
        {
            await _mediator.Send(new AddIngredientCommand(ingredientAddDto));

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] IngredientDto ingredientDto)
        {
            await _mediator.Send(new UpdateIngredientCommand(ingredientDto));

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _mediator.Send(new DeleteIngredientCommand(id));

            return new OkResult();
        }
    }
}
