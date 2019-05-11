using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
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

        public async Task<IEnumerable<OrderCustomerViewDto>> GetAllOrderForCustomerAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId)) throw new EntityNotFoundException($"Invalid customer Id:{customerId}");

            var ordersForCustomer = await Context
                                        .Orders
                                        .Include(o => o.Sandwiches)
                                        .Where(o => o.CustomerId == customerId)
                                        .ToListAsync() ?? throw new UnexpectedException();

            return ordersForCustomer.Select(order => order.MapTo(o => new OrderCustomerViewDto
            {
                Sandwiches = o.Sandwiches.Select(s => s.MapTo<Models.Entities.Sandwich, SandwichResultDto>()),
            }));
        }

        public async Task<IEnumerable<OrderWorkerViewDto>> GetPendingOrdersForWorkerAsync()
        {
            var ordersForWorkerView = await Context
                                          .Orders
                                          .Include(o => o.Sandwiches)
                                          .Include(o => o.Customer)
                                          .Where(o => o.State == OrderState.InTheOven || o.State == OrderState.OrderPlaced)
                                          .ToListAsync() ?? throw new UnexpectedException();

            return ordersForWorkerView.Select(order => order.MapTo(o => new OrderWorkerViewDto
            {
                Sandwiches = o.Sandwiches.Select(s => s.MapTo<Models.Entities.Sandwich, SandwichResultDto>())
            }));
        }

        public async Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            var order = await Context
                            .Orders
                            .SingleOrDefaultAsync(o => o.Id == updateOrderDto.OrderId)
                        ?? throw new EntityNotFoundException($"Order not found with id:{updateOrderDto.OrderId}");

            order.State = updateOrderDto.State;

            await AddOrUpdateAsync(order);
        }

        public async Task AddOrderAsync(OrderAddDto orderAddDto)
        {
            var user = await Context
                           .Users
                           .SingleOrDefaultAsync(u => u.Id == orderAddDto.UserId)
                       ?? throw new EntityNotFoundException($"User not found with id:{orderAddDto.UserId}");

            var sandwiches = new List<Models.Entities.Sandwich>();
            foreach (var id in orderAddDto.Sandwiches.Select(s => s.Id))
            {
                var sandwich = await Context.Sandwiches.SingleOrDefaultAsync(s => s.Id == id)
                               ?? throw new EntityNotFoundException($"Cant find sandwich with id:{id}");

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

            await AddOrUpdateAsync(newOrder);
        }

        public async Task DeleteOrderAsync(int orderId) => await DeleteAsync(orderId);
    }
}
