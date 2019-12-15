using AutoMapper;
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
    public sealed class GetAllOrderForCustomerQueryHandler : IRequestHandler<GetAllOrderForCustomerQuery, ICollection<CustomerViewOrderDto>>
    {
        private readonly ICocoricoDbContext _context;
        private readonly IMapper _mapper;

        public GetAllOrderForCustomerQueryHandler(
            IMapper mapper,
            ICocoricoDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ICollection<CustomerViewOrderDto>> Handle(GetAllOrderForCustomerQuery request, CancellationToken cancellationToken)
        {
            // TODO: Fluent Validator
            if (string.IsNullOrEmpty(request.CustomerId)) throw new EntityNotFoundException($"Invalid customer Id:{request.CustomerId}");

            var ordersForCustomer = await _context.Orders
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

            return _mapper.Map<ICollection<CustomerViewOrderDto>>(ordersForCustomer);
        }
    }
}