using System.Collections.Generic;
using Cocorico.Server.Domain.Models.Entities;
using Cocorico.Server.Domain.Services.Order;
using Cocorico.Server.Domain.Test.Helpers;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Test.Services
{
    [TestClass]
    public class OrderServiceTest : ServiceTestBase
    {
        [TestMethod]
        public async Task AddOrder()
        {
            var orderDto = await CreateOrder();

            using (var context = NewDbContext)
            {
                var actual = await context.Orders
                    .Include(o => o.SandwichLinks)
                    .ThenInclude(sl => sl.Sandwich)
                    .SingleAsync();

                var expected = orderDto.MapTo(s => new Order
                {
                    Id = 1,
                    Price = 150,
                    SandwichLinks = new List<UserSandwichOrder>(),
                });

                var dbSandwiches = await context
                    .Sandwiches
                    .ToListAsync();

                expected.SandwichLinks = dbSandwiches
                    .Where(s => orderDto.Sandwiches.Any(iDto => iDto.Id == s.Id))
                    .Select(s => new UserSandwichOrder()
                    {
                        Order = expected,
                        Sandwich = s,
                    })
                    .ToList();

                Assert.AreEqual(expected.Price, actual.Price);
                Assert.AreEqual(expected.Sandwiches().Count(), actual.Sandwiches().Count());
            }
        }

        [TestMethod]
        public async Task Remove()
        {
            await CreateOrder();

            using (var context = NewDbContext)
            {
                Assert.AreEqual(1, await context.Orders.CountAsync());

                var service = new ServerOrderService(context);

                await service.DeleteOrderAsync(1);

                Assert.AreEqual(0, await context.Orders.CountAsync());
            }
        }

        [TestMethod]
        public async Task GetPendingOrdersForWorker()
        {
            await CreateOrder();

            using (var context = NewDbContext)
            {
                var service = new ServerOrderService(context);
                var result = (await service.GetPendingOrdersForWorkerAsync()).ToList();

                Assert.AreEqual(1, result.Count);

                Assert.IsTrue(!string.IsNullOrEmpty(result.First().UserName));
            }
        }

        [TestMethod]
        public async Task GetAllOrderForCustomer()
        {
            var order = await CreateOrder();

            using (var context = NewDbContext)
            {
                var service = new ServerOrderService(context);
                var result = await service.GetAllOrderForCustomerAsync(order.CustomerId);

                Assert.AreEqual(1, result.Count());
            }
        }

        [TestMethod]
        public async Task UpdateOrderState()
        {
            await CreateOrder();

            using (var context = NewDbContext)
            {
                var service = new ServerOrderService(context);

                var order = await context.Orders.FirstAsync();
                await service.UpdateOrderAsync(new UpdateOrderDto
                {
                    OrderId = order.Id,
                    State = OrderState.InTheOven,
                });

                order = await context.Orders.FirstAsync();

                Assert.AreEqual(order.State, OrderState.InTheOven);
            }
        }

        private async Task<OrderAddDto> CreateOrder()
        {
            var user = SeedUsers();
            var sandwiches = SeedSandwiches();

            var orderDto = new OrderAddDto
            {
                UserId = user.Id,
                CustomerId = user.Id,
                Sandwiches = sandwiches.Select(s => new SandwichDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                }).ToList(),
            };

            using (var context = NewDbContext)
            {
                var service = new ServerOrderService(context);
                await service.AddOrderAsync(orderDto);
            }

            return orderDto;
        }
    }
}
