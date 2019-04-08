using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Cocorico.Client.ComponentModels.Sandwich
{
    public class SandwichesModel : ComponentBase
    {
        [Inject] private HttpClient HttpClient { get; set; }

        protected IReadOnlyList<SandwichResultDto> Sandwiches { get; private set; } = new List<SandwichResultDto>();

        protected override async Task OnInitAsync() => await LoadSandwichesAsync();

        private async Task LoadSandwichesAsync()
        {
            var response = await HttpClient.GetJsonAsync<IEnumerable<SandwichResultDto>>(Urls.Server.SandwichBase);

            Sandwiches = response.ToList();
        }
    }
}
