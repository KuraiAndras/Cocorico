using System;
using System.Collections.Generic;
using Cocorico.Shared.Services;

namespace Cocorico.Client.Domain.Services.Authentication
{
    public interface ICocoricoClientAuthenticationService : ICocoricoAuthenticationService
    {
        bool IsLoggedIn { get; }
        IEnumerable<string> Claims { get; }

        event Action UserLoggedIn;
        event Action UserLoggedOut;
    }
}
