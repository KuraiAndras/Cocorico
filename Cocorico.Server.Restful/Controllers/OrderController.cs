using Cocorico.Application.Orders.Commands.AddOrder;
using Cocorico.Application.Orders.Commands.DeleteOrder;
using Cocorico.Application.Orders.Commands.UpdateOrder;
using Cocorico.Application.Orders.Queries.CalculatePrice;
using Cocorico.Application.Orders.Queries.GetAllOrderForCustomer;
using Cocorico.Application.Orders.Queries.GetPendingOrdersForWorker;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Dtos.Orders;
using System;

namespace Cocorico.Server.Restful.Controllers
{
    [Produces(Verbs.ApplicationJson)]
    [Route(Verbs.ApiController)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<CocoricoUser> _userManager;

        public OrderController(
            IMediator mediator,
            UserManager<CocoricoUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Authorize(Policy = Policies.Customer)]
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<CustomerViewOrderDto>>> GetAllOrderForCustomerAsync([FromRoute] string customerId)
        {
            var userId = _userManager.GetUserId(HttpContext.User) ?? throw new InvalidCommandException();

            if (!userId.Equals(customerId, StringComparison.InvariantCulture)) throw new InvalidCommandException();

            var serviceResult = await _mediator.Send(new GetAllOrderForCustomerQuery(customerId));

            return new ActionResult<IEnumerable<CustomerViewOrderDto>>(serviceResult);
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpGet(nameof(GetPendingOrdersForWorkerAsync))]
        public async Task<ActionResult<IEnumerable<WorkerOrderViewDto>>> GetPendingOrdersForWorkerAsync()
        {
            var serviceResult = await _mediator.Send(new GetPendingOrdersForWorkerQuery());

            return new ActionResult<IEnumerable<WorkerOrderViewDto>>(serviceResult);
        }

        [Authorize(Policy = Policies.Customer)]
        [HttpPost]
        public async Task<ActionResult> AddOrderAsync([FromBody] AddOrderDto addOrderDto)
        {
            var userId = _userManager.GetUserId(HttpContext.User) ?? throw new InvalidCommandException();
            addOrderDto.UserId = userId;
            addOrderDto.CustomerId = userId;

            await _mediator.Send(new AddOrderCommand(addOrderDto));

            return new OkResult();
        }

        [Authorize]
        [HttpPost(nameof(CalculateOrderPriceAsync))]
        public async Task<ActionResult<int>> CalculateOrderPriceAsync([FromBody] AddOrderDto addOrderDto)
        {
            var result = await _mediator.Send(new CalculatePriceQuery(addOrderDto));

            return new ActionResult<int>(result);
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpDelete("{orderId:int}")]
        public async Task<ActionResult> DeleteOrderAsync([FromRoute] int orderId)
        {
            await _mediator.Send(new DeleteOrderCommand(orderId));

            return new OkResult();
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpPost(nameof(UpdateOrderAsync))]
        public async Task<ActionResult> UpdateOrderAsync([FromBody] UpdateOrderDto updateOrderDto)
        {
            await _mediator.Send(new UpdateOrderCommand(updateOrderDto));

            return new OkResult();
        }
    }
}
