using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using Cocorico.Domain.Exceptions;
using Cocorico.Shared.Dtos.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders.Queries.GetPendingOrdersForWorker
{
    public sealed class GetPendingOrdersForWorkerQueryHandler : IRequestHandler<GetPendingOrdersForWorkerQuery, ICollection<WorkerOrderViewDto>>
    {
        private readonly ICocoricoDbContext _context;
        private readonly IMapper _mapper;

        public GetPendingOrdersForWorkerQueryHandler(ICocoricoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<WorkerOrderViewDto>> Handle(GetPendingOrdersForWorkerQuery request, CancellationToken cancellationToken)
        {
            var ordersForWorkerView = await _context.Orders
                                          .Where(o => o.State != OrderState.Delivered && o.State != OrderState.Rejected)
                                          .Include(o => o.SandwichOrders)
                                          .ThenInclude(sl => sl.Sandwich)
                                          .ThenInclude(s => s.SandwichIngredients)
                                          .ThenInclude(il => il.Ingredient)
                                          .Include(o => o.SandwichOrders)
                                          .ThenInclude(so => so.IngredientModifications)
                                          .ThenInclude(im => im.Ingredient)
                                          .Include(o => o.CocoricoUser)
                                          .ToListAsync(cancellationToken) ?? throw new UnexpectedException();

            return _mapper.Map<ICollection<WorkerOrderViewDto>>(ordersForWorkerView);
        }
    }
}