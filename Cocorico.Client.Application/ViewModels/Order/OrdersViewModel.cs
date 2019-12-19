using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocorico.Client.Application.SignalrClient.WorkerOrders;
using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Entities;
using Cocorico.Shared.Exceptions;

namespace Cocorico.Client.Application.ViewModels.Order
{
    public class OrdersViewModel : IOrdersViewModel
    {
        private readonly IOrderClient _orderClient;
        private readonly IWorkerOrdersHubClient _hubClient;

        public OrdersViewModel(IOrderClient orderClient, IWorkerOrdersHubClient hubClient)
        {
            _orderClient = orderClient;
            _hubClient = hubClient;

            Orders = new List<WorkerOrderViewDto>();

            _hubClient.OrderAdded += order =>
            {
                Orders.Add(order);
                Orders.Sort(RotatingIdComparator);

                OrdersChanged?.Invoke();
            };

            _hubClient.OrderModified += order =>
            {
                var instance = Orders.SingleOrDefault(o => o.Id == order.Id);

                if (instance is null) throw new UnexpectedException();

                Orders.RemoveAll(o => o.Id == instance.Id);
                Orders.Add(order);
                Orders.Sort(RotatingIdComparator);

                OrdersChanged?.Invoke();
            };

            _hubClient.OrderDeleted += orderId =>
            {
                Orders.RemoveAll(o => o.Id == orderId);
                Orders.Sort(RotatingIdComparator);

                OrdersChanged?.Invoke();
            };
        }

        public List<WorkerOrderViewDto> Orders { get; }

        public async Task InitializeAsync()
        {
            try
            {
                await _hubClient.InitializeConnectionAsync();

                var orders = await _orderClient.GetPendingOrdersForWorkerAsync();

                Orders.Clear();
                Orders.AddRange(orders);
                Orders.Sort(RotatingIdComparator);
            }
            catch (Exception e)
            {
                // TODO: Handle exception
                Console.WriteLine(e);
            }
        }

        public async Task UpdateStateAsync(int orderId, OrderState newState)
        {
            try
            {
                var fileResponse = await _orderClient.UpdateOrderAsync(new UpdateOrderDto
                {
                    OrderId = orderId,
                    State = newState,
                });

                if (!fileResponse.IsSuccessfulStatusCode()) throw new InvalidOperationException();
            }
            catch (SwaggerException e)
            {
                //TODO: Handle fail
                Console.WriteLine(e);
            }
        }

        public event Action? OrdersChanged;

        private static int RotatingIdComparator(WorkerOrderViewDto o1, WorkerOrderViewDto o2) => o1.RotatingId - o2.RotatingId;
    }
}