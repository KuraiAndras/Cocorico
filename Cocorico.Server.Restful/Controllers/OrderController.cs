using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Models.Entities;
using Cocorico.Server.Domain.Services.Order;
using Cocorico.Server.Restful.Extensions;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Restful.Controllers
{
    [Produces(Verbs.ApplicationJson)]
    [Route(Verbs.ApiController)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly UserManager<CocoricoUser> _userManager;
        private readonly IServerOrderService _serverOrderService;

        public OrderController(
            UserManager<CocoricoUser> userManager,
            IServerOrderService serverOrderService)
        {
            _userManager = userManager;
            _serverOrderService = serverOrderService;
        }

        [Authorize(Policy = Policies.User)]
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderCustomerViewDto>>> GetAllOrderForCustomerAsync([FromRoute] string customerId)
        {
            var userId = _userManager.GetUserId(HttpContext.User) ?? throw new InvalidCommandException();

            if (!userId.Equals(customerId)) throw new InvalidCommandException();

            var serviceResult = await _serverOrderService.GetAllOrderForCustomerAsync(customerId);

            return new ActionResult<IEnumerable<OrderCustomerViewDto>>(serviceResult);
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpGet(Urls.ServerAction.PendingOrdersForWorker)]
        public async Task<ActionResult<IEnumerable<OrderWorkerViewDto>>> GetPendingOrdersForWorkerAsync()
        {
            var serviceResult = await _serverOrderService.GetPendingOrdersForWorkerAsync();

            return new ActionResult<IEnumerable<OrderWorkerViewDto>>(serviceResult);
        }

        //TODO: Worker policy update
        [Authorize(Policy = Policies.Customer)]
        [HttpPost]
        public async Task<ActionResult> AddOrderAsync([FromBody] OrderAddDto orderAddDto)
        {
            //TODO: Might change
            var userId = _userManager.GetUserId(HttpContext.User) ?? throw new InvalidCommandException();

            orderAddDto.UserId = userId;

            await _serverOrderService.AddOrderAsync(orderAddDto);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpDelete("{orderId:int}")]
        public async Task<ActionResult> DeleteOrderAsync([FromRoute] int orderId)
        {
            await _serverOrderService.DeleteOrderAsync(orderId);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpPost("UpdateOrder")]
        public async Task<ActionResult> UpdateOrderAsync([FromBody]UpdateOrderDto updateOrderDto)
        {
            await _serverOrderService.UpdateOrderAsync(updateOrderDto);

            return new OkResult();
        }
    }
}
