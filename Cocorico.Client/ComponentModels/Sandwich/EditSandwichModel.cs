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
        // ReSharper disable once MemberCanBePrivate.Global
        [Parameter] protected int SandwichId { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private HttpClient HttpClient { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        protected SandwichResultDto Sandwich { get; private set; } = new SandwichResultDto();

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
            var result = await HttpClient.PostAsync(Urls.Server.SandwichBase, new StringContent(Json.Serialize(Sandwich), Encoding.UTF8, Verbs.ApplicationJson));

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
