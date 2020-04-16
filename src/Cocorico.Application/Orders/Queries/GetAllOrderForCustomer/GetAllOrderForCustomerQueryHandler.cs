﻿using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Dtos.Orders;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders.Queries.GetAllOrderForCustomer
{
    public sealed class GetAllOrderForCustomerQueryHandler : QueryHandlerBase<GetAllOrderForCustomerQuery, ICollection<CustomerViewOrderDto>>
    {
        public GetAllOrderForCustomerQueryHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<ICollection<CustomerViewOrderDto>> Handle(GetAllOrderForCustomerQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Dto)) throw new EntityNotFoundException($"Invalid customer Id:{request.Dto}");

            var ordersForCustomer = await Context.Orders
                                        .Include(o => o.SandwichOrders)
                                        .ThenInclude(sl => sl.Sandwich)
                                        .ThenInclude(s => s.SandwichIngredients)
                                        .ThenInclude(il => il.Ingredient)
                                        .Include(o => o.SandwichOrders)
                                        .ThenInclude(so => so.IngredientModifications)
                                        .ThenInclude(im => im.Ingredient)
                                        .Where(o => o.CocoricoUserId == request.Dto)
                                        .ToListAsync(cancellationToken)
                                    ?? throw new UnexpectedException();

            return Mapper.Map<ICollection<CustomerViewOrderDto>>(ordersForCustomer);
        }
    }
}