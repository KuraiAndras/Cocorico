using Cocorico.Application.Orders.Services.RotatingId;
using Cocorico.Shared.Api;
using Cocorico.Shared.Api.Openings;
using Cocorico.Shared.Api.Orders;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Controllers
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
            var result = await _mediator.Send(new GetAllOpenings());

            return new ActionResult<ICollection<OpeningDto>>(result);
        }

        [HttpGet(nameof(IsStoreOpenAsync))]
        public async Task<ActionResult<bool>> IsStoreOpenAsync()
        {
            var result = await _mediator.Send(new CanAddOrder { RequestTime = DateTime.Now });

            return new ActionResult<bool>(result);
        }

        [HttpPost(nameof(AddOpeningAsync))]
        public async Task<ActionResult> AddOpeningAsync([FromBody] AddOpening addOpening)
        {
            await _mediator.Send(addOpening);

            return new OkResult();
        }

        [HttpPost(nameof(UpdateOpeningAsync))]
        public async Task<ActionResult> UpdateOpeningAsync([FromBody] UpdateOpening openingDto)
        {
            await _mediator.Send(openingDto);

            return new OkResult();
        }

        [HttpDelete(nameof(DeleteOpeningAsync))]
        public async Task<ActionResult> DeleteOpeningAsync([FromBody] int openingId)
        {
            await _mediator.Send(new DeleteOpening { Id = openingId });

            return new OkResult();
        }
    }
}
