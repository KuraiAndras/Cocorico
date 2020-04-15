using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.SignalrClient
{
    public abstract class SignalrClientBase : IHubClient
    {
        protected readonly HubConnection _connection;

        protected SignalrClientBase(
            HubConnectionBuilder hubConnectionBuilder,
            string url)
        {
            _connection = hubConnectionBuilder.WithUrl(url).Build();
        }

        public async Task InitializeConnectionAsync() => await _connection.StartAsync();
    }
}