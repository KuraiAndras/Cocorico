using Cocorico.DAL.Models;
using Cocorico.DAL.Models.Entities;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.Order;
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

        public async Task AddOrderAsync(AddOrderDto addOrderDto)
        {
            var user = await Context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == addOrderDto.UserId)
                       ?? throw new EntityNotFoundException($"User not found with id:{addOrderDto.UserId}");

            //TODO: This might change
            var sandwichesInDb = await Context
                .Sandwiches
                .AsNoTracking()
                .Include(s => s.SandwichIngredients)
                .ThenInclude(il => il.Ingredient)
                .AsNoTracking()
                .ToListAsync();

            var sandwiches = addOrderDto.Sandwiches
                .Select(sandwichDto => sandwichesInDb.SingleOrDefault(s => s.Id == sandwichDto.Id))
                .Where(s => s != null)
                .ToList();

            if (sandwiches.Count == 0) throw new EntityNotFoundException();

            var newOrder = new Order
            {
                Id = 0,
                CocoricoUserId = user.Id,
                Price = sandwiches.Select(s => s.Price).Aggregate((sum, price) => sum + price),
                State = OrderState.OrderPlaced,
            };

            await AddAsync(newOrder);
        }

        public async Task DeleteOrderAsync(int orderId) => await DeleteByIdAsync(orderId);
    }
}
