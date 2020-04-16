using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.NavMenu
{
    public interface INavMenuViewModel
    {
        string NavMenuCssClass { get; }
        void ToggleNavMenu();
        Task LogoutAsync();
    }
}
