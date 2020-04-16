using Cocorico.Shared.Hubs;

namespace Cocorico.Client.SignalrClient.WorkerOrders
{
    public interface IWorkerOrdersHubClientListener : IHubClient
    {
        void RegisterListener(IWorkerViewOrderClient client);
    }
}
