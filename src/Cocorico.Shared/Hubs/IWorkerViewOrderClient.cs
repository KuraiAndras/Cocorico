using Cocorico.Shared.Api.Orders;
using System.Threading.Tasks;

namespace Cocorico.Shared.Hubs
{
    public interface IWorkerViewOrderClient
    {
        Task ReceiveOrderAddedAsync(WorkerOrderViewDto order);
        Task ReceiveOrderModifiedAsync(WorkerOrderViewDto order);
        Task ReceiveOrderDeletedAsync(int orderId);
    }
}
