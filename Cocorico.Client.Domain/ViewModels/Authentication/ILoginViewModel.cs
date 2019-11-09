using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Authentication;

namespace Cocorico.Client.Domain.ViewModels.Authentication
{
    public interface ILoginViewModel
    {
        bool ShowLoginFailed { get; }
        LoginDetails UserLoginDetails { get; }
        Task LoginUserAsync();
    }
}
