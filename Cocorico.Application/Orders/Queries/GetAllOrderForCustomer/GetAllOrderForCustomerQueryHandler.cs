﻿using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Exceptions;
using Cocorico.Shared.Dtos.Order;
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
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<ICollection<CustomerViewOrderDto>> Handle(GetAllOrderForCustomerQuery request, CancellationToken cancellationToken)
        {
            // TODO: Fluent Validator
            if (string.IsNullOrEmpty(request.CustomerId)) throw new EntityNotFoundException($"Invalid customer Id:{request.CustomerId}");

            var ordersForCustomer = await Context.Orders
                                        .Include(o => o.SandwichOrders)
                                        .ThenInclude(sl => sl.Sandwich)
                                        .ThenInclude(s => s.SandwichIngredients)
                                        .ThenInclude(il => il.Ingredient)
                                        .Include(o => o.SandwichOrders)
                                        .ThenInclude(so => so.IngredientModifications)
                                        .ThenInclude(im => im.Ingredient)
                                        .Where(o => o.CocoricoUserId == request.CustomerId)
                                        .ToListAsync(cancellationToken)
                                    ?? throw new UnexpectedException();

            return Mapper.Map<ICollection<CustomerViewOrderDto>>(ordersForCustomer);
        }
    }
}