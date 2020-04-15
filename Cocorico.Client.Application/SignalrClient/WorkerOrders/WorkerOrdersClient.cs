using Cocorico.Shared.Dtos.Orders;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Hubs;
using Microsoft.AspNetCore.SignalR.Client;

namespace Cocorico.Client.Application.SignalrClient.WorkerOrders
{
    public class WorkerOrdersHubClient : SignalrClientBase, IWorkerOrdersHubClientListener
    {
        public WorkerOrdersHubClient(HubConnectionBuilder hubConnectionBuilder)
            : base(hubConnectionBuilder, HubNames.WorkerViewOrderHubNames.Name)
        {
        }

        public void RegisterListener(IWorkerViewOrderClient client)
        {
            Connection.On<WorkerOrderViewDto>(HubNames.WorkerViewOrderHubNames.ReceiveOrderAddedAsync, client.ReceiveOrderAddedAsync);

            Connection.On<WorkerOrderViewDto>(HubNames.WorkerViewOrderHubNames.ReceiveOrderModifiedAsync, client.ReceiveOrderModifiedAsync);

            Connection.On<int>(HubNames.WorkerViewOrderHubNames.ReceiveOrderDeletedAsync, client.ReceiveOrderDeletedAsync);
        }
    }
}
