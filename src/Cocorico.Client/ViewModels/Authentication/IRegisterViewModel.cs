using Cocorico.Shared.Dtos.Authentication;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Authentication
{
    public interface IRegisterViewModel
    {
        bool ShowRegisterFailed { get; }
        RegisterDetails UserRegisterDetails { get; }

        Task RegisterUserAsync();
    }
}
