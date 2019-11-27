using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Order
{
    public class OrdersViewModel : IOrdersViewModel
    {
        private readonly IOrderClient _orderClient;
        private readonly HubConnection _connection;

        public OrdersViewModel(IOrderClient orderClient)
        {
            _orderClient = orderClient;

            Orders = new List<WorkerOrderViewDto>();

            _connection = new HubConnectionBuilder()
                .WithUrl(HubNames.WorkerViewOrderHubNames.Name, opt => opt.Transports = HttpTransportType.WebSockets)
                .Build();

            _connection.On<WorkerOrderViewDto[]>(HubNames.WorkerViewOrderHubNames.ReceiveOrdersAsync, OnOrdersModifiedAsync);

            Task OnOrdersModifiedAsync(WorkerOrderViewDto[] orders)
            {
                Orders.Clear();
                Orders.AddRange(orders);

                Console.WriteLine(orders.First().Id);
                Console.WriteLine(orders.Skip(1).First().Id);
                Console.WriteLine(orders.Skip(2).First().Id);

                OrdersChanged?.Invoke();

                return Task.CompletedTask;
            }
        }

        public List<WorkerOrderViewDto> Orders { get; }

        public async Task InitializeAsync()
        {
            try
            {
                await _connection.StartAsync();

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