using System.Linq;
using System.Threading.Tasks;
using Cocorico.Client.Domain.Services.Sandwich;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Components;

namespace Cocorico.Client.Blazor.ComponentModels.Sandwich
{
    public class EditSandwichModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Parameter] private int SandwichId { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IClientSandwichService SandwichService { get; set; }

        protected NewSandwichDto Sandwich { get; private set; } = new NewSandwichDto();

        protected override async Task OnInitAsync()
        {
            var result = await SandwichService.GetSandwichResultAsync(SandwichId);

            switch (result)
            {
                case Success<SandwichResultDto> success:
                    Sandwich = success.Data.MapTo<SandwichResultDto, NewSandwichDto>();
                    break;
            }
        }

        protected async Task Edit()
        {
            var result = await SandwichService.AddOrUpdateSandwichAsync(Sandwich);

            switch (result)
            {
                case Success _:
                    UriHelper.NavigateTo(Urls.Client.GetAllSandwich);
                    break;
            }
        }
    }
}
