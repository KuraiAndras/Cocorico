using Cocorico.Application.Sandwiches.Commands.AddSandwich;
using Cocorico.Application.Sandwiches.Commands.DeleteSandwich;
using Cocorico.Application.Sandwiches.Commands.UpdateSandwich;
using Cocorico.Application.Sandwiches.Queries.GetAllSandwich;
using Cocorico.Application.Sandwiches.Queries.GetSandwich;
using Cocorico.Domain.Identity;
using Cocorico.Shared.Dtos.Sandwich;
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
    public class SandwichController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SandwichController(IMediator mediator) => _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SandwichDto>>> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllSandwichQuery());

            return new ActionResult<IEnumerable<SandwichDto>>(result);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SandwichDto>> GetAsync([FromRoute] int id)
        {
            var serviceResult = await _mediator.Send(new GetSandwichQuery(id));

            return new ActionResult<SandwichDto>(serviceResult);
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] SandwichDto sandwich)
        {
            await _mediator.Send(new UpdateSandwichCommand(sandwich));

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] SandwichAddDto sandwich)
        {
            await _mediator.Send(new AddSandwichCommand(sandwich));

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _mediator.Send(new DeleteSandwichCommand(id));

            return new OkResult();
        }
    }
}
