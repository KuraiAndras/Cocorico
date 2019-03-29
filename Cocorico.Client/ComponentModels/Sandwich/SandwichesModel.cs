using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cocorico.Client.ComponentModels.Sandwich
{
    public class SandwichesModel : ComponentBase
    {
        [Inject] private HttpClient _httpClient { get; set; }

        protected IReadOnlyList<SandwichResultDto> sandwiches { get; private set; } = new List<SandwichResultDto>();

        protected override async Task OnInitAsync() => await LoadSandwichesAsync();

        private async Task LoadSandwichesAsync()
        {
            var response = await _httpClient.GetJsonAsync<IEnumerable<SandwichResultDto>>(Urls.Server.GetAllSandwich);

            sandwiches = response.ToList();
        }
    }
}
