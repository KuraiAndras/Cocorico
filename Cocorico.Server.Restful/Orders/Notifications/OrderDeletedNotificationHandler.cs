using Cocorico.Application.Orders.Notifications.OrderAdded;
using Cocorico.Server.Restful.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Server.Restful.Orders.Notifications
{
    public class OrderDeletedNotificationHandler : INotificationHandler<OrderDeletedEvent>
    {
        private readonly IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> _workerViewHub;

        public OrderDeletedNotificationHandler(
            IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> workerViewHub)
            => _workerViewHub = workerViewHub;

        public async Task Handle(OrderDeletedEvent notification, CancellationToken cancellationToken) =>
            await _workerViewHub.Clients.All.ReceiveOrderDeletedAsync(notification.Dto);
    }
}