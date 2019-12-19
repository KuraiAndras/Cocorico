using System;
using System.Threading.Tasks;
using Blazor.Extensions;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;

namespace Cocorico.Client.Application.SignalrClient.WorkerOrders
{
    public class WorkerOrdersHubClient : SignalrClientBase, IWorkerOrdersHubClient
    {
        public WorkerOrdersHubClient(HubConnectionBuilder hubConnectionBuilder)
            : base(hubConnectionBuilder, HubNames.WorkerViewOrderHubNames.Name)
        {
            _connection.On<WorkerOrderViewDto>(HubNames.WorkerViewOrderHubNames.ReceiveOrderAddedAsync, o =>
            {
                OrderAdded?.Invoke(o);

                return Task.CompletedTask;
            });

            _connection.On<WorkerOrderViewDto>(HubNames.WorkerViewOrderHubNames.ReceiveOrderModifiedAsync, o =>
            {
                OrderModified?.Invoke(o);

                return Task.CompletedTask;
            });

            _connection.On<int>(HubNames.WorkerViewOrderHubNames.ReceiveOrderDeletedAsync, o =>
            {
                OrderDeleted?.Invoke(o);

                return Task.CompletedTask;
            });
        }

        public event Action<WorkerOrderViewDto>? OrderAdded;
        public event Action<WorkerOrderViewDto>? OrderModified;
        public event Action<int>? OrderDeleted;
    }
}
