using Cocorico.Shared.Api.Sandwiches;
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
    public class SandwichController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SandwichController(IMediator mediator) => _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SandwichDto>>> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllSandwiches());

            return new ActionResult<IEnumerable<SandwichDto>>(result);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SandwichDto>> GetAsync([FromRoute] int id)
        {
            var serviceResult = await _mediator.Send(new GetSandwich { Id = id });

            return new ActionResult<SandwichDto>(serviceResult);
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateSandwich sandwich)
        {
            await _mediator.Send(sandwich);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] AddSandwich addSandwich)
        {
            await _mediator.Send(addSandwich);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Administrator)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _mediator.Send(new DeleteSandwich { SandwichId = id });

            return new OkResult();
        }
    }
}
