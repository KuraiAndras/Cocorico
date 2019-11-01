using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Models.Entities;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.Order;
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
                                        .Include(o => o.SandwichLinks)
                                        .ThenInclude(sl => sl.Sandwich)
                                        .ThenInclude(s => s.IngredientLinks)
                                        .ThenInclude(il => il.Ingredient)
                                        .Where(o => o.CustomerId == customerId)
                                        .ToListAsync()
                                    ?? throw new UnexpectedException();

            return ordersForCustomer.Select(order => order.MapTo(o => new OrderCustomerViewDto
            {
                Sandwiches = o.Sandwiches().Select(s => s.ToSandwichDto()),
            }));
        }

        public async Task<IEnumerable<OrderWorkerViewDto>> GetPendingOrdersForWorkerAsync()
        {
            var ordersForWorkerView = await Context.Orders
                                          .Include(o => o.SandwichLinks)
                                          .ThenInclude(sl => sl.Sandwich)
                                          .ThenInclude(s => s.IngredientLinks)
                                          .ThenInclude(il => il.Ingredient)
                                          .Include(o => o.Customer)
                                          .Where(o => o.State != OrderState.Delivered)
                                          .ToListAsync() ?? throw new UnexpectedException();

            return ordersForWorkerView.Select(order => order.ToOrderWorkerViewDto());
        }

        public async Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            var order = await Context.Orders
                            .Include(o => o.SandwichLinks)
                            .ThenInclude(sl => sl.Sandwich)
                            .ThenInclude(s => s.IngredientLinks)
                            .ThenInclude(il => il.Ingredient)
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
            var allSandwich = await Context
                .Sandwiches
                .Include(s => s.IngredientLinks)
                .ThenInclude(il => il.Ingredient)
                .ToListAsync();

            var sandwiches = allSandwich.Where(s => !(orderAddDto.Sandwiches.SingleOrDefault(os => os.Id == s.Id) is null)).ToList();

            var newOrder = new Models.Entities.Order
            {
                Id = 0,
                CustomerId = user.Id,
                Customer = user,
                Price = sandwiches.Select(s => s.Price).Aggregate((sum, price) => sum + price),
                State = OrderState.OrderPlaced,
            };

            newOrder.SandwichLinks = sandwiches
                .Select(s => new SandwichOrder
                {
                    Order = newOrder,
                    Sandwich = s,
                })
                .ToList();

            await AddAsync(newOrder);
        }

        public async Task DeleteOrderAsync(int orderId) => await DeleteByIdAsync(orderId);
    }
}
