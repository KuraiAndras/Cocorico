using AutoMapper;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Orders;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Controllers
{
    [Produces(Verbs.ApplicationJson)]
    [Route(Verbs.ApiController)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<CocoricoUser> _userManager;
        private readonly IMapper _mapper;

        public OrderController(
            IMediator mediator,
            UserManager<CocoricoUser> userManager,
            IMapper mapper)
        {
            _mediator = mediator;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize(Policy = Policies.Customer)]
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<CustomerViewOrderDto>>> GetAllOrderForCustomerAsync([FromRoute] string customerId)
        {
            var userId = _userManager.GetUserId(HttpContext.User) ?? throw new InvalidCommandException();

            if (!userId.Equals(customerId, StringComparison.InvariantCulture)) throw new InvalidCommandException();

            var serviceResult = await _mediator.Send(new GetAllOrderForCustomer { CustomerId = customerId });

            return new ActionResult<IEnumerable<CustomerViewOrderDto>>(serviceResult);
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpGet(nameof(GetPendingOrdersForWorkerAsync))]
        public async Task<ActionResult<IEnumerable<WorkerOrderViewDto>>> GetPendingOrdersForWorkerAsync()
        {
            var serviceResult = await _mediator.Send(new GetPendingOrdersForWorker());

            return new ActionResult<IEnumerable<WorkerOrderViewDto>>(serviceResult);
        }

        [Authorize(Policy = Policies.Customer)]
        [HttpPost]
        public async Task<ActionResult> AddOrderAsync([FromBody] AddOrder addOrder)
        {
            var userId = _userManager.GetUserId(HttpContext.User) ?? throw new InvalidCommandException();
            addOrder.UserId = userId;
            addOrder.CustomerId = userId;

            await _mediator.Send(addOrder);

            return new OkResult();
        }

        [Authorize]
        [HttpPost(nameof(CalculateOrderPriceAsync))]
        public async Task<ActionResult<int>> CalculateOrderPriceAsync([FromBody] AddOrder addOrder)
        {
            var result = await _mediator.Send(_mapper.Map<CalculatePrice>(addOrder));

            return new ActionResult<int>(result);
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpDelete("{orderId:int}")]
        public async Task<ActionResult> DeleteOrderAsync([FromRoute] int orderId)
        {
            await _mediator.Send(new DeleteOrder { Id = orderId });

            return new OkResult();
        }

        [Authorize(Policy = Policies.Worker)]
        [HttpPost(nameof(UpdateOrderAsync))]
        public async Task<ActionResult> UpdateOrderAsync([FromBody] UpdateOrder updateOrder)
        {
            await _mediator.Send(updateOrder);

            return new OkResult();
        }
    }
}
