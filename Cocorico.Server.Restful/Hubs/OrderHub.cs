using Cocorico.DAL.Models.Entities;
using Cocorico.Server.Domain.Services.SystemNotifications;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Restful.Hubs
{
    [Authorize(Roles = Claims.User)]
    public class OrderHub : Hub
    {
        private readonly ConcurrentDictionary<string, List<string>> _connectionRoles;

        public OrderHub(ISystemNotifier<Order> orderNotifier)
        {
            _connectionRoles = new ConcurrentDictionary<string, List<string>>();

            orderNotifier.OnNotification += async order => await Clients
                .AllExcept(GetConnectionsWithoutClaims(_connectionRoles, Claims.Worker))
                .SendAsync("OrderModified", order);
        }

        public override Task OnConnectedAsync()
        {
            _connectionRoles.TryAdd(Context.ConnectionId, Context.User.Claims.Select(c => c.Value).ToList());

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _connectionRoles.TryRemove(Context.ConnectionId, out _);

            return base.OnDisconnectedAsync(exception);
        }

        private static IReadOnlyList<string> GetConnectionsWithoutClaims(ConcurrentDictionary<string, List<string>> claimsDictionary, params string[] claimValues) =>
            claimsDictionary
                .Where(cr => !cr.Value.Any(r => claimValues.Any(c => string.Equals(c, r))))
                .Select(cr => cr.Key)
                .ToList();
    }
}
