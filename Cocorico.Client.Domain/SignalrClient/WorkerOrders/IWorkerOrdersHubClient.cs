using System;
using Cocorico.Shared.Dtos.Order;

namespace Cocorico.Client.Domain.SignalrClient.WorkerOrders
{
    public interface IWorkerOrdersHubClient : IHubClient
    {
        event Action<WorkerOrderViewDto> OrderAdded;
    }
}