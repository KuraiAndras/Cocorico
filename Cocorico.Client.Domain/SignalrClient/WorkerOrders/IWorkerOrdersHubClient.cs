using Cocorico.Shared.Dtos.Order;
using System;

namespace Cocorico.Client.Domain.SignalrClient.WorkerOrders
{
    public interface IWorkerOrdersHubClient : IHubClient
    {
        event Action<WorkerOrderViewDto> OrderAdded;
        event Action<WorkerOrderViewDto> OrderModified;
        event Action<int> OrderDeleted;
    }
}