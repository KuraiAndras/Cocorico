using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Services.OrderService;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public SettingsController(IOrderRotatingIdService orderRotatingIdService) =>
            _orderRotatingIdService = orderRotatingIdService;

        [HttpGet]
        public Task<ActionResult<MutableRange>> CurrentRangeAsync()
        {
            var currentRange = _orderRotatingIdService.GetRange();
            return Task.FromResult(new ActionResult<MutableRange>(new MutableRange
            {
                End = currentRange.End.Value,
                Start = currentRange.Start.Value
            }));
        }

        [HttpPost]
        public ActionResult SetCurrentRange([FromBody] MutableRange newRange)
        {
            _orderRotatingIdService.SetRange(new Range(newRange.Start, newRange.End));

            return new OkResult();
        }
    }
}
