using Cocorico.Application.Orders.Notifications.OrderAdded;
using Cocorico.Server.Hubs;
using Cocorico.Shared.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Server.Orders.Notifications
{
    public class OrderModifiedNotificationHandler : INotificationHandler<OrderModifiedEvent>
    {
        private readonly IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> _workerViewHub;

        public OrderModifiedNotificationHandler(
            IHubContext<WorkerViewOrderHub, IWorkerViewOrderClient> workerViewHub) =>
            _workerViewHub = workerViewHub;

        public async Task Handle(OrderModifiedEvent notification, CancellationToken cancellationToken) =>
            await _workerViewHub.Clients.All.ReceiveOrderModifiedAsync(notification.Dto);
    }
}
