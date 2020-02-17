using System;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Hubs;

namespace Cocorico.Client.Application.SignalrClient.WorkerOrders
{
    public interface IWorkerOrdersHubClientListener : IHubClient
    {
        void RegisterListener(IWorkerViewOrderClient client);
    }
}