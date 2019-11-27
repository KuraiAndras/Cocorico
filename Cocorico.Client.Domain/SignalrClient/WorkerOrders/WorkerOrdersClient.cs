using Blazor.Extensions;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;
using System;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.SignalrClient.WorkerOrders
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
        }

        public event Action<WorkerOrderViewDto>? OrderAdded;
    }
}
