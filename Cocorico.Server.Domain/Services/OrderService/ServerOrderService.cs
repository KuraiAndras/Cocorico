using Cocorico.DAL.Models;
using Cocorico.DAL.Models.Entities;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.OrderService
{
    public class ServerOrderService : EntityServiceBase<Order>, IServerOrderService
    {
        public ServerOrderService(CocoricoDbContext context) : base(context)
        {
        }

        public async Task<ICollection<CustomerViewOrderDto>> GetAllOrderForCustomerAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId)) throw new EntityNotFoundException($"Invalid customer Id:{customerId}");

            var ordersForCustomer = await Context.Orders
                                        .Include(o => o.SandwichOrders)
                                        .ThenInclude(sl => sl.Sandwich)
                                        .ThenInclude(s => s.SandwichIngredients)
                                        .ThenInclude(il => il.Ingredient)
                                        .Where(o => o.CocoricoUserId == customerId)
                                        .ToListAsync()
                                    ?? throw new UnexpectedException();

            return ordersForCustomer.Select(order => order.MapTo(o => new CustomerViewOrderDto
            {
                Sandwiches = o.Sandwiches().Select(s => s.ToSandwichDto()).ToList(),
            })).ToList();
        }

        public async Task<ICollection<WorkerOrderViewDto>> GetPendingOrdersForWorkerAsync()
        {
            var ordersForWorkerView = await Context.Orders
                                          .Include(o => o.SandwichOrders)
                                          .ThenInclude(sl => sl.Sandwich)
                                          .ThenInclude(s => s.SandwichIngredients)
                                          .ThenInclude(il => il.Ingredient)
                                          .Include(o => o.CocoricoUser)
                                          .Where(o => o.State != OrderState.Delivered)
                                          .ToListAsync() ?? throw new UnexpectedException();

            return ordersForWorkerView.Select(order => order.ToOrderWorkerViewDto()).ToList();
        }

        public async Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            var order = await Context.Orders
                            .Include(o => o.SandwichOrders)
                            .ThenInclude(sl => sl.Sandwich)
                            .ThenInclude(s => s.SandwichIngredients)
                            .ThenInclude(il => il.Ingredient)
                            .SingleOrDefaultAsync(o => o.Id == updateOrderDto.OrderId)
                        ?? throw new EntityNotFoundException($"Order not found with id:{updateOrderDto.OrderId}");

            order.State = updateOrderDto.State;

            await UpdateAsync(order);
        }

        public async Task<int> AddOrderAsync(AddOrderDto addOrderDto)
        {
            var sandwichesInDb = await Context
                .Sandwiches
                .ToListAsync();

            var sandwiches = addOrderDto.Sandwiches
                .Select(sandwichDto => sandwichesInDb.SingleOrDefault(s => s.Id == sandwichDto.Id))
                .Where(s => s != null)
                .ToList();

            if (sandwiches.Count == 0) throw new EntityNotFoundException();

            var newOrder = new Order
            {
                CocoricoUserId = addOrderDto.CustomerId,
                Price = sandwiches.Select(s => s.Price).Aggregate((sum, price) => sum + price),
                State = OrderState.OrderPlaced,
                SandwichOrders = new List<SandwichOrder>(),
            };

            foreach (var sandwich in sandwiches)
            {
                newOrder.SandwichOrders.Add(new SandwichOrder
                {
                    Order = newOrder,
                    Sandwich = sandwich,
                });
            }

            await Context.Orders.AddAsync(newOrder);

            await Context.SaveChangesAsync();

            return newOrder.Id;
        }

        public async Task DeleteOrderAsync(int orderId) => await DeleteByIdAsync(orderId);
    }
}
