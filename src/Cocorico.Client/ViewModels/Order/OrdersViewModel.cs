using Cocorico.Client.Extensions;
using Cocorico.Client.HttpClient;
using Cocorico.Client.SignalrClient.WorkerOrders;
using Cocorico.Shared.Api.Orders;
using Cocorico.Shared.Entities;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Order
{
    public class OrdersViewModel : IOrdersViewModel, IWorkerViewOrderClient
    {
        private readonly IOrderClient _orderClient;
        private readonly IWorkerOrdersHubClientListener _hubClient;

        public OrdersViewModel(IOrderClient orderClient, IWorkerOrdersHubClientListener hubClient)
        {
            _orderClient = orderClient;
            _hubClient = hubClient;

            Orders = new List<WorkerOrderViewDto>();

            _hubClient.RegisterListener(this);
        }

        public event Action? OrdersChanged;

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
            catch (Exception)
            {
            }
        }

        public async Task UpdateStateAsync(int orderId, OrderState newState)
        {
            try
            {
                var fileResponse = await _orderClient.UpdateOrderAsync(new UpdateOrder
                {
                    OrderId = orderId,
                    State = newState,
                });

                if (!fileResponse.IsSuccessfulStatusCode()) throw new InvalidOperationException();
            }
            catch (SwaggerException)
            {
            }
        }

        public Task ReceiveOrderAddedAsync(WorkerOrderViewDto order)
        {
            Orders.Add(order);
            Orders.Sort(RotatingIdComparator);

            OrdersChanged?.Invoke();

            return Task.CompletedTask;
        }

        public Task ReceiveOrderModifiedAsync(WorkerOrderViewDto order)
        {
            var instance = Orders.SingleOrDefault(o => o.Id == order.Id);

            if (instance is null) throw new UnexpectedException();

            Orders.RemoveAll(o => o.Id == instance.Id);
            Orders.Add(order);
            Orders.Sort(RotatingIdComparator);

            OrdersChanged?.Invoke();

            return Task.CompletedTask;
        }

        public Task ReceiveOrderDeletedAsync(int orderId)
        {
            Orders.RemoveAll(o => o.Id == orderId);
            Orders.Sort(RotatingIdComparator);

            OrdersChanged?.Invoke();

            return Task.CompletedTask;
        }

        private static int RotatingIdComparator(WorkerOrderViewDto x, WorkerOrderViewDto y) => x.RotatingId - y.RotatingId;
    }
}
