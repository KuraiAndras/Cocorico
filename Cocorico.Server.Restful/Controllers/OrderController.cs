using Cocorico.Domain.Entities;
using Cocorico.Domain.Exceptions;
using Cocorico.Domain.Identity;
using Cocorico.Server.Domain.Services.OrderService;
using Cocorico.Server.Restful.Hubs;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> _workerViewHub;

        public OrderController(
            UserManager<CocoricoUser> userManager,
            IServerOrderService serverOrderService,
            IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> workerViewHub)
        {
            _userManager = userManager;
            _serverOrderService = serverOrderService;
            _workerViewHub = workerViewHub;
        }

        [Authorize(Policy = Policies.Customer)]
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<CustomerViewOrderDto>>> GetAllOrderForCustomerAsync([FromRoute] string customerId)
        {
            var userId = _userManager.GetUserId(HttpContext.User) ?? throw new InvalidCommandException();

            if (!userId.Equals(customerId)) throw new InvalidCommandException();

            var serviceResult = await _serverOrderService.GetAllOrderForCustomerAsync(customerId);

            return new ActionResult<IEnumerable<CustomerViewOrderDto>>(serviceResult);
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpGet(nameof(GetPendingOrdersForWorkerAsync))]
        public async Task<ActionResult<IEnumerable<WorkerOrderViewDto>>> GetPendingOrdersForWorkerAsync()
        {
            var serviceResult = await _serverOrderService.GetPendingOrdersForWorkerAsync();

            return new ActionResult<IEnumerable<WorkerOrderViewDto>>(serviceResult);
        }

        //TODO: Worker policy update
        [Authorize(Policy = Policies.Customer)]
        [HttpPost]
        public async Task<ActionResult> AddOrderAsync([FromBody] AddOrderDto addOrderDto)
        {
            var userId = _userManager.GetUserId(HttpContext.User) ?? throw new InvalidCommandException();
            addOrderDto.UserId = userId;
            addOrderDto.CustomerId = userId;

            var result = await _serverOrderService.AddOrderAsync(addOrderDto);

            var orderView = (await _serverOrderService.GetPendingOrdersForWorkerAsync())
                .SingleOrDefault(o => o.Id == result);

            await _workerViewHub.ReceiveOrderAddedImplementationAsync(orderView);

            return new OkResult();
        }

        [Authorize]
        [HttpPost(nameof(CalculateOrderPriceAsync))]
        public async Task<ActionResult<int>> CalculateOrderPriceAsync([FromBody] AddOrderDto addOrderDto)
        {
            var result = await _serverOrderService.CalculatePriceAsync(addOrderDto);

            return new ActionResult<int>(result);
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpDelete("{orderId:int}")]
        public async Task<ActionResult> DeleteOrderAsync([FromRoute] int orderId)
        {
            await _serverOrderService.DeleteOrderAsync(orderId);

            await _workerViewHub.ReceiveOrderDeletedImplementationAsync(orderId);

            return new OkResult();
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpPost(nameof(UpdateOrderAsync))]
        public async Task<ActionResult> UpdateOrderAsync([FromBody] UpdateOrderDto updateOrderDto)
        {
            await _serverOrderService.UpdateOrderAsync(updateOrderDto);

            var updatedPendingOrder = (await _serverOrderService.GetPendingOrdersForWorkerAsync())
                .SingleOrDefault(o => o.Id == updateOrderDto.OrderId);

            if (updatedPendingOrder is null)
            {
                await _workerViewHub.ReceiveOrderDeletedImplementationAsync(updateOrderDto.OrderId);
            }
            else
            {
                await _workerViewHub.ReceiveOrderModifiedImplementationAsync(updatedPendingOrder);
            }

            return new OkResult();
        }
    }
}
