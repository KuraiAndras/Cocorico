using Cocorico.Shared.Dtos.Authentication;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Authentication
{
    public interface ILoginViewModel
    {
        bool ShowLoginFailed { get; }
        LoginDetails UserLoginDetails { get; }
        Task LoginUserAsync();
    }
}
