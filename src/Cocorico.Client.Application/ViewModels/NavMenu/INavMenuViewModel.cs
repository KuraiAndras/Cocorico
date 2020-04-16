using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.NavMenu
{
    public interface INavMenuViewModel
    {
        string NavMenuCssClass { get; }
        void ToggleNavMenu();
        Task LogoutAsync();
    }
}
