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
        [HttpGet("customer/{customerId:int}")]
        public async Task<IActionResult> GetAllOrderForCustomerAsync([FromRoute] string customerId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if (userId is null) return new Fail<IEnumerable<OrderCustomerViewDto>>(new InvalidCommandException()).ToActionResult();

            if (!userId.Equals(customerId)) return new Fail<IEnumerable<OrderCustomerViewDto>>(new InvalidCommandException()).ToActionResult();

            var serviceResult = await _serverOrderService.GetAllOrderForCustomerAsync(customerId);

            return serviceResult.ToActionResult();
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpGet(Urls.ServerAction.PendingOrdersForWorker)]
        public async Task<IActionResult> GetPendingOrdersForWorkerAsync()
        {
            var serviceResult = await _serverOrderService.GetPendingOrdersForWorkerAsync();

            return serviceResult.ToActionResult();
        }

        //TODO: Worker policy update
        [Authorize(Policy = Policies.Customer)]
        [HttpPost]
        public async Task<IActionResult> AddOrderAsync([FromBody] OrderAddDto orderAddDto)
        {
            //TODO: Might change
            var userId = _userManager.GetUserId(HttpContext.User);
            if (userId is null) return new Fail(new InvalidCommandException()).ToActionResult();

            orderAddDto.UserId = userId;

            //Authorize
            var serviceResult = await _serverOrderService.AddOrderAsync(orderAddDto);

            return serviceResult.ToActionResult();
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpDelete("{orderId:int}")]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] int orderId)
        {
            var serviceResult = await _serverOrderService.DeleteOrderAsync(orderId);

            return serviceResult.ToActionResult();
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpPost("UpdateOrder")]
        public async Task<IActionResult> UpdateOrderAsync([FromBody]UpdateOrderDto updateOrderDto)
        {
            var serviceResult = await _serverOrderService.UpdateOrderAsync(updateOrderDto);

            return serviceResult.ToActionResult();
        }
    }
}
