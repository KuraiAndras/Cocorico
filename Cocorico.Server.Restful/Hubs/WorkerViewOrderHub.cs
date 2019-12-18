using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Cocorico.Server.Restful.Hubs
{
    [Authorize(Roles = Claims.Worker)]
    public class WorkerViewOrderHub : Hub<IWorkerViewOrderClient>
    {
    }

    public interface IWorkerViewOrderClient
    {
        Task ReceiveOrderAddedAsync(WorkerOrderViewDto order);
        Task ReceiveOrderModifiedAsync(WorkerOrderViewDto order);
        Task ReceiveOrderDeletedAsync(int orderId);
    }
}
