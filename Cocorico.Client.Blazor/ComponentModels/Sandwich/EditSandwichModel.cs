using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Sandwich
{
    public class EditSandwichModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Parameter] private int SandwichId { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private ISandwichClient SandwichHttpClient { get; set; }

        protected SandwichDto Sandwich { get; private set; } = new SandwichDto();

        protected override async Task OnInitAsync()
        {
            try
            {
                var sandwichResultDto = await SandwichHttpClient.GetAsync(SandwichId);
                Sandwich = sandwichResultDto;
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        protected async Task Edit()
        {
            try
            {
                var fileResponse = await SandwichHttpClient.UpdateAsync(Sandwich);

                //TODO: Handle fail
                if (!fileResponse.IsSuccessfulStatusCode()) return;

                UriHelper.NavigateTo(Urls.Client.GetAllSandwich);
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}
