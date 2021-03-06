﻿using Cocorico.Application.Orders.Notifications;
using Cocorico.Server.Hubs;
using Cocorico.Shared.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Server.Orders.Notifications
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
