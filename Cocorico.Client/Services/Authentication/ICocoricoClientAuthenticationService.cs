using Cocorico.Shared.Services;
using System;
using System.Collections.Generic;

namespace Cocorico.Client.Services.Authentication
{
    public interface ICocoricoClientAuthenticationService : ICocoricoAuthenticationService
    {
        bool IsLoggedIn { get; }
        IEnumerable<string> Claims { get; }

        event Action UserLoggedIn;
        event Action UserLoggedOut;
    }
}
