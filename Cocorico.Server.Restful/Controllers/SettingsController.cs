using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Services.Opening;
using Cocorico.Server.Domain.Services.OrderService;
using Cocorico.Shared.Dtos.Opening;
using Cocorico.Shared.Helpers;
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
        private readonly IOpeningService _openingService;

        public SettingsController(
            IOrderRotatingIdService orderRotatingIdService,
            IOpeningService openingService)
        {
            _orderRotatingIdService = orderRotatingIdService;
            _openingService = openingService;
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
            var result = await _openingService.GetAllOpeningsAsync();

            return new ActionResult<ICollection<OpeningDto>>(result);
        }

        [HttpGet(nameof(IsStoreOpenAsync))]
        public async Task<ActionResult<bool>> IsStoreOpenAsync()
        {
            var result = await _openingService.CanAddOrderAsync(DateTime.Now);

            return new ActionResult<bool>(result);
        }

        [HttpPost(nameof(AddOpening))]
        public async Task<ActionResult> AddOpening([FromBody] AddOpeningDto addOpeningDto)
        {
            await _openingService.AddOpening(addOpeningDto);

            return new OkResult();
        }

        [HttpPost(nameof(UpdateOpeningAsync))]
        public async Task<ActionResult> UpdateOpeningAsync([FromBody] OpeningDto openingDto)
        {
            await _openingService.UpdateOpening(openingDto);

            return new OkResult();
        }

        [HttpDelete(nameof(DeleteOpeningAsync))]
        public async Task<ActionResult> DeleteOpeningAsync([FromBody] int openingId)
        {
            await _openingService.DeleteOpening(openingId);

            return new OkResult();
        }
    }
}
