using Cocorico.Client.Extensions;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cocorico.Client.Helpers;

namespace Cocorico.Client.Services.Sandwich
{
    public class ClientSandwichService : IClientSandwichService
    {
        private readonly HttpClient _httpClient;

        public ClientSandwichService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IServiceResult<SandwichResultDto>> GetSandwichResultAsync(int id)
        {
            var result = await _httpClient.RetrieveFromServerAsync<SandwichResultDto>(HttpVerbs.Get, Urls.Server.SandwichBase + $"/{id}");

            return result;
        }

        public async Task<IServiceResult<IEnumerable<SandwichResultDto>>> GetAllSandwichResultAsync()
        {
            var result = await _httpClient.RetrieveFromServerAsync<IEnumerable<SandwichResultDto>>(HttpVerbs.Get, Urls.Server.SandwichBase);

            return result;
        }

        public async Task<IServiceResult> AddOrUpdateSandwichAsync(NewSandwichDto newSandwichDto)
        {
            var result = await _httpClient.PostJsonWithResultAsync(Urls.Server.SandwichBase, newSandwichDto);

            return result.GetServiceResult(new InvalidCredentialsException());
        }

        public async Task<IServiceResult> DeleteSandwichAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(Urls.Server.SandwichBase + $"/{id}");

            return response.GetServiceResult();
        }
    }
}
