using System.Threading.Tasks;

namespace Cocorico.Client.Domain.SignalrClient
{
    public interface IHubClient
    {
        Task InitializeConnectionAsync();
    }
}