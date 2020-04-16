using Cocorico.Shared.Dtos.Authentication;
using System.Threading.Tasks;

namespace Cocorico.Client.Services.Authentication
{
    public interface ICocoricoAuthenticationStateProvider
    {
        Task LoginAsync(LoginDetails loginDetails);
        Task LogoutAsync();
    }
}