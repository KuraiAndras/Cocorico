using System.Threading.Tasks;

namespace Cocorico.Client.Application.SignalrClient
{
    public interface IHubClient
    {
        Task InitializeConnectionAsync();
    }
}