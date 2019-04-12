using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cocorico.Client.Services.Authentication;
using Microsoft.AspNetCore.Components.Services;
using Microsoft.JSInterop;

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Cocorico.Client.ComponentModels.Sandwich
{
    public class SandwichesModel : ComponentBase
    {
        [Inject] private HttpClient HttpClient { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] protected IUserAuthenticationService UserAuthenticationService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] private IUriHelper UriHelper { get; set; }
        protected IReadOnlyList<SandwichResultDto> Sandwiches { get; private set; } = new List<SandwichResultDto>();

        protected override async Task OnInitAsync() => await LoadSandwichesAsync();

        private async Task LoadSandwichesAsync()
        {
            var response = await HttpClient.GetAsync(Urls.Server.SandwichBase);

            if (response.IsSuccessStatusCode)
            {
                var sandwiches = Json.Deserialize<IEnumerable<SandwichResultDto>>(await response.Content.ReadAsStringAsync());
                Sandwiches = sandwiches.ToList();
            }
            else
            {
                //TODO: Handle fail
            }
        }

        protected void Edit(int sandwichId)
        {
            UriHelper.NavigateTo(Urls.Client.EditSandwich + $"/{sandwichId}");
        }

        protected async Task Delete(int sandwichId)
        {
            var response = await HttpClient.DeleteAsync(Urls.Server.SandwichBase + $"/{sandwichId}");

            if (response.IsSuccessStatusCode)
            {
                await LoadSandwichesAsync();
            }
        }
    }
}
