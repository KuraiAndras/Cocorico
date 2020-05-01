using Cocorico.Shared.Api.Users;
using System.Threading.Tasks;

namespace Cocorico.Client.Services.Authentication
{
    public interface ICocoricoAuthenticationStateProvider
    {
        Task LoginAsync(LoginUser loginDetails);
        Task LogoutAsync();
    }
}
