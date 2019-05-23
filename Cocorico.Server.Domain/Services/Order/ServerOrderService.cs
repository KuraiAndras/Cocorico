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

            var ordersForCustomer = await Context.Orders
                                        .Include(o => o.Sandwiches)
                                        .Where(o => o.CustomerId == customerId)
                                        .ToListAsync()
                                    ?? throw new UnexpectedException();

            return ordersForCustomer.Select(order => order.MapTo(o => new OrderCustomerViewDto
            {
                Sandwiches = o.Sandwiches.Select(s => s.MapTo<Models.Entities.Sandwich, SandwichDto>()),
            }));
        }

        public async Task<IEnumerable<OrderWorkerViewDto>> GetPendingOrdersForWorkerAsync()
        {
            var ordersForWorkerView = await Context.Orders
                                          .Include(o => o.Sandwiches)
                                          .Include(o => o.Customer)
                                          .Where(o => o.State != OrderState.Delivered)
                                          .ToListAsync() ?? throw new UnexpectedException();

            return ordersForWorkerView.Select(order => order.MapTo(o => new OrderWorkerViewDto
            {
                UserName = o.Customer.Name,
                Sandwiches = o.Sandwiches.Select(s => s.MapTo<Models.Entities.Sandwich, SandwichDto>())
            }));
        }

        public async Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            var order = await Context.Orders
                            .Include(o => o.Sandwiches)
                            .SingleOrDefaultAsync(o => o.Id == updateOrderDto.OrderId)
                        ?? throw new EntityNotFoundException($"Order not found with id:{updateOrderDto.OrderId}");

            order.State = updateOrderDto.State;

            await UpdateAsync(order);
        }

        public async Task AddOrderAsync(OrderAddDto orderAddDto)
        {
            var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == orderAddDto.UserId)
                       ?? throw new EntityNotFoundException($"User not found with id:{orderAddDto.UserId}");

            //TODO: This might change
            var allSandwich = await Context.Sandwiches.ToListAsync();
            var sandwiches = allSandwich.Where(s => !(orderAddDto.Sandwiches.SingleOrDefault(os => os.Id == s.Id) is null)).ToList();

            var newOrder = new Models.Entities.Order
            {
                Id = 0,
                CustomerId = user.Id,
                Customer = user,
                Price = sandwiches.Select(s => s.Price).Aggregate((sum, price) => sum + price),
                Sandwiches = sandwiches,
                State = OrderState.OrderPlaced,
            };

            await AddAsync(newOrder);
        }

        public async Task DeleteOrderAsync(int orderId) => await DeleteByIdAsync(orderId);
    }
}
