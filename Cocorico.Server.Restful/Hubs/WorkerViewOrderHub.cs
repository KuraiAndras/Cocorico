using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;
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
    }

    public static class IWorkerViewOrderClientExtensions
    {
        public static async Task ReceiveOrderAddedImplementationAsync(
            this IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> hub,
            WorkerOrderViewDto order) =>
            await hub.Clients.All.ReceiveOrderAddedAsync(order);
    }
}
