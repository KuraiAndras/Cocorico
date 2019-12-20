using System;
using Cocorico.Shared.Dtos.Order;

namespace Cocorico.Client.Application.SignalrClient.WorkerOrders
{
    public interface IWorkerOrdersHubClient : IHubClient
    {
        event Action<WorkerOrderViewDto> OrderAdded;
        event Action<WorkerOrderViewDto> OrderModified;
        event Action<int> OrderDeleted;
    }
}