using Cocorico.Server.Domain.Models.Entities;
using Cocorico.Server.Domain.Services.Order;
using Cocorico.Server.Domain.Test.Helpers;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
                var actual = await context.Orders.SingleAsync();

                var expected = orderDto.MapTo(s => new Order
                {
                    Id = 1,
                    Price = 300,
                });

                Assert.AreEqual(expected, actual);
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
                var result = await service.GetPendingOrdersForWorkerAsync();


                var success = (Success<IEnumerable<OrderWorkerViewDto>>)result;
                Assert.AreEqual(1, success.Data.Count());
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

                var success = (Success<IEnumerable<OrderCustomerViewDto>>)result;
                Assert.AreEqual(1, success.Data.Count());
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
                Sandwiches = sandwiches.Select(s => new SandwichResultDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                })
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
