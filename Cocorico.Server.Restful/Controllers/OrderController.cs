using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Services.Order;
using Cocorico.Server.Restful.Extensions;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Order;

namespace Cocorico.Server.Restful.Controllers
{
    [Produces(Verbs.ApplicationJson)]
    [Route(Verbs.ApiController)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IServerOrderService _serverOrderService;

        public OrderController(IServerOrderService serverOrderService)
        {
            _serverOrderService = serverOrderService;
        }

        //TODO: new claim for self edit
        [Authorize(Policy = Policies.User)]
        [HttpGet("customer/{customerId:int}")]
        public async Task<IActionResult> GetAllOrderForCustomerAsync([FromRoute] string customerId)
        {
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
    }
}
