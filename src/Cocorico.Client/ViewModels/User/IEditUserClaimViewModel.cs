using Cocorico.Shared.Dtos.User;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.User
{
    public interface IEditUserClaimViewModel
    {
        UserForAdminPage UserForAdminPage { get; }

        Task AddClaimToUserAsync(string claimValue, string userId);
        Task LoadUserAsync(string userId);
        Task RemoveClaimFromUserAsync(string claimValue, string userId);
    }
}
