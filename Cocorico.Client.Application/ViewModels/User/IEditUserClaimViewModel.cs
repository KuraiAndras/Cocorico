using System.Threading.Tasks;
using Cocorico.Shared.Dtos.User;

namespace Cocorico.Client.Application.ViewModels.User
{
    public interface IEditUserClaimViewModel
    {
        UserForAdminPage UserForAdminPage { get; }

        Task AddClaimToUserAsync(string claimValue, string userId);
        Task LoadUserAsync(string userId);
        Task RemoveClaimFromUserAsync(string claimValue, string userId);
    }
}
