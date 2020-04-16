using Cocorico.Application.Orders.Notifications.OrderAdded;
using Cocorico.Server.Restful.Hubs;
using Cocorico.Shared.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Server.Restful.Orders.Notifications
{
    public sealed class OrderAddedNotificationHandler : INotificationHandler<OrderAddedEvent>
    {
        private readonly IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> _workerViewHub;

        public OrderAddedNotificationHandler(IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> workerViewHub) =>
            _workerViewHub = workerViewHub;

        public async Task Handle(OrderAddedEvent notification, CancellationToken cancellationToken) =>
            await _workerViewHub.Clients.All.ReceiveOrderAddedAsync(notification.Dto);
    }
}
