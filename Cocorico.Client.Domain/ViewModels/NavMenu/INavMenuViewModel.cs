using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.NavMenu
{
    public interface INavMenuViewModel
    {
        string NavMenuCssClass { get; }
        bool IsLoggedIn { get; set; }
        bool IsCustomer { get; }
        bool IsWorker { get; }
        bool IsAdmin { get; }

        void Initialize();
        Task LogoutAsync();
        void ToggleNavMenu();
    }
}
