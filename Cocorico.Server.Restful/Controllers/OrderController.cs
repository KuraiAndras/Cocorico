using Cocorico.DAL.Models.Entities;
using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Services.OrderService;
using Cocorico.Server.Restful.Hubs;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Exceptions;
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

        [Authorize(Policy = Policies.User)]
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
            //TODO: Might change
            addOrderDto.UserId = _userManager.GetUserId(HttpContext.User) ?? throw new InvalidCommandException();

            await _serverOrderService.AddOrderAsync(addOrderDto);

            var orders = (await _serverOrderService.GetPendingOrdersForWorkerAsync()).ToArray();
            await _workerViewHub.Clients.All.ReceiveOrdersAsync(orders);

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
        [HttpPost(nameof(UpdateOrderAsync))]
        public async Task<ActionResult> UpdateOrderAsync([FromBody] UpdateOrderDto updateOrderDto)
        {
            await _serverOrderService.UpdateOrderAsync(updateOrderDto);

            return new OkResult();
        }
    }
}
