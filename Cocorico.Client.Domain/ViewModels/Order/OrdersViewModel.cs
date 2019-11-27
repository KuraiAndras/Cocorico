using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Client.Domain.SignalrClient.WorkerOrders;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Order
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

            _hubClient.OrderAdded += o => Orders.Add(o);
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
    }
}