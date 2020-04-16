using System.Threading.Tasks;

namespace Cocorico.Client.SignalrClient
{
    public interface IHubClient
    {
        Task InitializeConnectionAsync();
    }
}