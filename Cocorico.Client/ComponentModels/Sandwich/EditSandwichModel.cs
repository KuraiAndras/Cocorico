using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using Microsoft.JSInterop;

namespace Cocorico.Client.ComponentModels.Sandwich
{
    public class EditSandwichModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Parameter] protected int SandwichId { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] HttpClient HttpClient { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        protected SandwichResultDto Sandwich { get; set; }

        protected override async Task OnInitAsync()
        {
            var result = await HttpClient.GetAsync(Urls.Server.SandwichBase + $"/{SandwichId}");

            if (result.IsSuccessStatusCode)
            {
                Sandwich = Json.Deserialize<SandwichResultDto>(await result.Content.ReadAsStringAsync());
            }
            else
            {
                //TODO: Handle fail
            }
        }

        protected async Task Edit()
        {
            var result = await HttpClient.PostAsync(Urls.Server.SandwichBase + $"/{Sandwich.Id}", new StringContent(Json.Serialize(Sandwich), Encoding.UTF8, Verbs.ApplicationJson));

            if (result.IsSuccessStatusCode)
            {
                UriHelper.NavigateTo(Urls.Client.GetAllSandwich);
            }
            else
            {
                //TODO: Show fail
            }
        }
    }
}
