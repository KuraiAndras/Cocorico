using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.NavMenu
{
    public interface INavMenuViewModel
    {
        string NavMenuCssClass { get; }
        void ToggleNavMenu();
        Task LogoutAsync();
    }
}
