using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Services.Authentication
{
    public interface IUserAuthenticationService
    {
        Task<IServiceResult> RegisterAsync(RegisterDetails registerDetails);
        Task<IServiceResult> LoginAsync(LoginDetails loginDetails);
        Task<IServiceResult> LogoutAsync();

        bool IsLoggedIn { get; }
        IEnumerable<string> Claims { get; }

        event Action UserLoggedIn;
        event Action UserLoggedOut;
    }
}
