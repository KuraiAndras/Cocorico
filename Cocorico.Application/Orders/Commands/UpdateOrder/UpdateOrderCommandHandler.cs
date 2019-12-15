using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocorico.Application.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : AsyncRequestHandler<UpdateOrderCommand>
    {
        private readonly ICocoricoDbContext _context;

        public UpdateOrderCommandHandler(ICocoricoDbContext context, IMapper mapper) => _context = context;

        protected override async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                            .SingleOrDefaultAsync(o => o.Id == request.Dto.OrderId, cancellationToken)
                        ?? throw new EntityNotFoundException($"Order not found with id:{request.Dto.OrderId}");

            order.State = request.Dto.State;

            _context.Orders.Update(order);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}