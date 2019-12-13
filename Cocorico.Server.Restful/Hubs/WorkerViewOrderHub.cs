using Cocorico.Domain.Identity;
using Cocorico.Shared.Dtos.Order;
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

    public static class IWorkerViewOrderClientExtensions
    {
        public static async Task ReceiveOrderAddedImplementationAsync(
            this IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> hub,
            WorkerOrderViewDto order) =>
            await hub.Clients.All.ReceiveOrderAddedAsync(order);

        public static async Task ReceiveOrderModifiedImplementationAsync(
            this IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> hub,
            WorkerOrderViewDto order) =>
            await hub.Clients.All.ReceiveOrderModifiedAsync(order);

        public static async Task ReceiveOrderDeletedImplementationAsync(
            this IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> hub,
            int orderId) =>
            await hub.Clients.All.ReceiveOrderDeletedAsync(orderId);
    }
}
