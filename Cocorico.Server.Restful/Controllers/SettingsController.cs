using Cocorico.Application.Openings.Commands.AddOpening;
using Cocorico.Application.Openings.Commands.DeleteOpening;
using Cocorico.Application.Openings.Commands.UpdateOpening;
using Cocorico.Application.Openings.Queries.GetAllOpenings;
using Cocorico.Application.Orders.Queries.CanAddOrder;
using Cocorico.Application.Orders.Services.RotatingId;
using Cocorico.Shared.Dtos;
using Cocorico.Shared.Dtos.Opening;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Restful.Controllers
{
    [Produces(Verbs.ApplicationJson)]
    [Route(Verbs.ApiController)]
    [ApiController]
    [Authorize(Policy = Policies.Administrator)]
    public class SettingsController : ControllerBase
    {
        private readonly IOrderRotatingIdService _orderRotatingIdService;
        private readonly IMediator _mediator;

        public SettingsController(
            IOrderRotatingIdService orderRotatingIdService,
            IMediator mediator)
        {
            _orderRotatingIdService = orderRotatingIdService;
            _mediator = mediator;
        }

        [HttpGet(nameof(CurrentRangeAsync))]
        public Task<ActionResult<MutableRange>> CurrentRangeAsync()
        {
            var currentRange = _orderRotatingIdService.GetRange();
            return Task.FromResult(new ActionResult<MutableRange>(new MutableRange
            {
                End = currentRange.End.Value,
                Start = currentRange.Start.Value
            }));
        }

        [HttpPost(nameof(SetCurrentRange))]
        public ActionResult SetCurrentRange([FromBody] MutableRange newRange)
        {
            _orderRotatingIdService.SetRange(new Range(newRange.Start, newRange.End));

            return new OkResult();
        }

        [HttpGet(nameof(GetAllOpeningsAsync))]
        public async Task<ActionResult<ICollection<OpeningDto>>> GetAllOpeningsAsync()
        {
            var result = await _mediator.Send(new GetAllOpeningsQuery());

            return new ActionResult<ICollection<OpeningDto>>(result);
        }

        [HttpGet(nameof(IsStoreOpenAsync))]
        public async Task<ActionResult<bool>> IsStoreOpenAsync()
        {
            var result = await _mediator.Send(new CanAddOrderQuery(DateTime.Now));

            return new ActionResult<bool>(result);
        }

        [HttpPost(nameof(AddOpening))]
        public async Task<ActionResult> AddOpening([FromBody] AddOpeningDto addOpeningDto)
        {
            await _mediator.Send(new AddOpeningCommand(addOpeningDto));

            return new OkResult();
        }

        [HttpPost(nameof(UpdateOpeningAsync))]
        public async Task<ActionResult> UpdateOpeningAsync([FromBody] OpeningDto openingDto)
        {
            await _mediator.Send(new UpdateOpeningCommand(openingDto));

            return new OkResult();
        }

        [HttpDelete(nameof(DeleteOpeningAsync))]
        public async Task<ActionResult> DeleteOpeningAsync([FromBody] int openingId)
        {
            await _mediator.Send(new DeleteOpeningCommand(openingId));

            return new OkResult();
        }
    }
}
