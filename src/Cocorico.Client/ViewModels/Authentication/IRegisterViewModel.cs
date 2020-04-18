using Cocorico.Shared.Api.Users;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Authentication
{
    public interface IRegisterViewModel
    {
        bool ShowRegisterFailed { get; }
        RegisterUser UserRegisterUser { get; }

        Task RegisterUserAsync();
    }
}
