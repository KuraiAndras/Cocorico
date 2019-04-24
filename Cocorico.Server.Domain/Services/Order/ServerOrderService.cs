using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.Order
{
    public class ServerOrderService : EntityServiceBase<Models.Entities.Order>, IServerOrderService
    {
        public ServerOrderService(CocoricoDbContext context) : base(context)
        {
        }

        public async Task<IServiceResult<IEnumerable<OrderCustomerViewDto>>> GetAllOrderForCustomerAsync(string customerId)
        {
            if (customerId is null) return new Fail<IEnumerable<OrderCustomerViewDto>>(new InvalidCommandException());
            var ordersForCustomer = await Context
                .Orders
                .IncludeSandwiches()
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();

            return new Success<IEnumerable<OrderCustomerViewDto>>(ordersForCustomer.Select(o => new OrderCustomerViewDto
            {
                Id = o.Id,
                Price = o.Price,
                Sandwiches = o.Sandwiches.Select(s => new SandwichResultDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                }),
                State = o.State,
            }));
        }

        public async Task<IServiceResult<IEnumerable<OrderWorkerViewDto>>> GetPendingOrdersForWorkerAsync()
        {
            var ordersForWorkerView = await Context
                .Orders
                .IncludeSandwiches()
                .Include(o => o.Customer)
                .Where(o => o.State == OrderState.InTheOven || o.State == OrderState.OrderPlaced)
                .ToListAsync();

            return new Success<IEnumerable<OrderWorkerViewDto>>(ordersForWorkerView.Select(o => new OrderWorkerViewDto
            {
                Id = o.Id,
                Sandwiches = o.Sandwiches.Select(s => new SandwichResultDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                }),
                State = o.State,
                UserName = o.Customer.UserName,
            }));
        }

        public async Task<IServiceResult> AddOrderAsync(OrderAddDto orderAddDto)
        {
            var user = await Context
                .Users
                .SingleOrDefaultAsync(u => u.Id == orderAddDto.UserId);
            if (user is null) return new Fail(new EntityNotFoundException());

            var sandwiches = new List<Models.Entities.Sandwich>();
            foreach (var id in orderAddDto.Sandwiches.Select(s => s.Id))
            {
                var sandwich = await Context.Sandwiches.SingleOrDefaultAsync(s => s.Id == id);
                if (sandwich is null) return new Fail(new EntityNotFoundException());

                sandwiches.Add(sandwich);
            }

            var newOrder = new Models.Entities.Order
            {
                Id = 0,
                CustomerId = user.Id,
                Customer = user,
                Price = sandwiches.Select(s => s.Price).Aggregate((sum, price) => sum + price),
                Sandwiches = sandwiches,
                State = OrderState.OrderPlaced,
            };

            var addResult = await AddOrUpdateAsync(newOrder);

            return addResult;
        }

        public async Task<IServiceResult> DeleteOrderAsync(int orderId) => await DeleteAsync(orderId);
    }

    internal static class ContextOrderExtension
    {
        internal static IQueryable<Models.Entities.Order> IncludeSandwiches(this IQueryable<Models.Entities.Order> queryable) =>
            queryable
                .Include(o => o.Sandwiches);
    }
}
