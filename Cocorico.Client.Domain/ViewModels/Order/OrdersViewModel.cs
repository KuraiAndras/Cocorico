using Blazor.Extensions;
using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
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
        private readonly HubConnection _connection;

        public OrdersViewModel(IOrderClient orderClient, HubConnectionBuilder hubConnectionBuilder)
        {
            _orderClient = orderClient;

            Orders = new List<WorkerOrderViewDto>();

            _connection = hubConnectionBuilder
                .WithUrl(HubNames.WorkerViewOrderHubNames.Name, options =>
                {
                    options.LogLevel = SignalRLogLevel.Trace;
                    options.Transport = HttpTransportType.WebSockets;
                })
                .Build();

            _connection.On<WorkerOrderViewDto>(HubNames.WorkerViewOrderHubNames.ReceiveOrderAddedAsync, OnOrdersModifiedAsync);

            Task OnOrdersModifiedAsync(WorkerOrderViewDto order)
            {
                Orders.Add(order);

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