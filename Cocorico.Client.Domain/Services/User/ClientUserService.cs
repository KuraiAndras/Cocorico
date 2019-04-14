using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.Services.User
{
    public class ClientUserService : IClientUserService
    {
        private readonly HttpClient _httpClient;

        public ClientUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IServiceResult<IEnumerable<UserForAdminPage>>> GetAllUsersForAdminPageAsync()
        {
            var result = await _httpClient.RetrieveDataFromServerAsync<IEnumerable<UserForAdminPage>>(HttpVerbs.Get, Urls.Server.GetAllUsersForAdminPage, new InvalidCredentialsException());

            return result;
        }
    }
}
