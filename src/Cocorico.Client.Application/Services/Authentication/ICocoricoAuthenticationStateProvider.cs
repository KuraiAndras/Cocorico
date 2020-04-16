using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Authentication;

namespace Cocorico.Client.Application.Services.Authentication
{
    public interface ICocoricoAuthenticationStateProvider
    {
        Task LoginAsync(LoginDetails loginDetails);
        Task LogoutAsync();
    }
}