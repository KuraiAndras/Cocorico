using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Cocorico.Client.SignalrClient
{
    public abstract class SignalrClientBase : IHubClient
    {
#pragma warning disable CA1054 // Uri parameters should not be strings
        protected SignalrClientBase(HubConnectionBuilder hubConnectionBuilder, string url) =>
            Connection = hubConnectionBuilder.WithUrl(url).Build();
#pragma warning restore CA1054 // Uri parameters should not be strings

        protected HubConnection Connection { get; }

        public async Task InitializeConnectionAsync() => await Connection.StartAsync();
    }
}
