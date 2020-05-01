using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Api.Orders;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders
{
    public sealed class GetAllOrderForCustomerHandler : RequestHandlerBase<GetAllOrderForCustomer, ICollection<CustomerViewOrderDto>>
    {
        public GetAllOrderForCustomerHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<ICollection<CustomerViewOrderDto>> Handle(GetAllOrderForCustomer request, CancellationToken cancellationToken)
        {
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
                .ToListAsync(cancellationToken);

            return Mapper.Map<ICollection<CustomerViewOrderDto>>(ordersForCustomer);
        }
    }
}
