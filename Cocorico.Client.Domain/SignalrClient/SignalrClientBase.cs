using System.Threading.Tasks;
using Blazor.Extensions;

namespace Cocorico.Client.Domain.SignalrClient
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