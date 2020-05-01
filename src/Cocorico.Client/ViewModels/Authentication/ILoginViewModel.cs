using Cocorico.Shared.Api.Users;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Authentication
{
    public interface ILoginViewModel
    {
        bool ShowLoginFailed { get; }
        LoginUser UserLoginDetails { get; }
        Task LoginUserAsync();
    }
}
