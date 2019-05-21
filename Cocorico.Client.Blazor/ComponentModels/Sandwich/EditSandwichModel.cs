using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Sandwich
{
    public class EditSandwichModel : ComponentBase
    {
        [Parameter] private int SandwichId { get; set; }
        [Inject] private IUriHelper UriHelper { get; set; }
        [Inject] private ISandwichClient SandwichHttpClient { get; set; }

        protected NewSandwichDto Sandwich { get; private set; } = new NewSandwichDto();

        protected override async Task OnInitAsync()
        {
            try
            {
                var sandwichResultDto = await SandwichHttpClient.GetAsync(SandwichId);
                Sandwich = sandwichResultDto.MapTo<SandwichResultDto, NewSandwichDto>();
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
