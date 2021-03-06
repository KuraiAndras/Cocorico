﻿using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Api.Orders;
using Cocorico.Shared.Entities;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders
{
    public sealed class GetPendingOrdersForWorkerRequestHandler : RequestHandlerBase<GetPendingOrdersForWorker, ICollection<WorkerOrderViewDto>>
    {
        public GetPendingOrdersForWorkerRequestHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<ICollection<WorkerOrderViewDto>> Handle(GetPendingOrdersForWorker request, CancellationToken cancellationToken)
        {
            var ordersForWorkerView = await Context
                .Orders
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

            return Mapper.Map<ICollection<WorkerOrderViewDto>>(ordersForWorkerView);
        }
    }
}
