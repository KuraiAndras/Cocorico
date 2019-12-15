using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Application.Orders.Notifications.OrderAdded;
using Cocorico.Domain.Exceptions;
using Cocorico.Shared.Dtos.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : AsyncRequestHandler<UpdateOrderCommand>
    {
        private readonly ICocoricoDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(
            ICocoricoDbContext context,
            IMediator mediator,
            IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        protected override async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                            .SingleOrDefaultAsync(o => o.Id == request.Dto.OrderId, cancellationToken)
                        ?? throw new EntityNotFoundException($"Order not found with id:{request.Dto.OrderId}");

            order.State = request.Dto.State;

            _context.Orders.Update(order);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new OrderModifiedEvent(_mapper.Map<WorkerOrderViewDto>(order)), cancellationToken);
        }
    }
}