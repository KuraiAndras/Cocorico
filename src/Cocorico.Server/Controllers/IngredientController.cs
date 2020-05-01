using Cocorico.Shared.Api.Ingredients;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Controllers
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
            new ActionResult<IEnumerable<IngredientDto>>(await _mediator.Send(new GetAllIngredients()));

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IngredientDto>> GetAsync([FromRoute] int id) =>
            new ActionResult<IngredientDto>(await _mediator.Send(new GetIngredient { Id = id }));

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] AddIngredient addIngredient)
        {
            await _mediator.Send(addIngredient);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateIngredient ingredientDto)
        {
            await _mediator.Send(ingredientDto);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _mediator.Send(new DeleteIngredient { Id = id });

            return new OkResult();
        }
    }
}
