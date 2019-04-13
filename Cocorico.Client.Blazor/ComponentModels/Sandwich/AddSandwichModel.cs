using System.Threading.Tasks;
using Cocorico.Client.Domain.Services.Sandwich;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;

namespace Cocorico.Client.Blazor.ComponentModels.Sandwich
{
    public class AddSandwichModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IClientSandwichService SandwichService { get; set; }

        protected NewSandwichDto NewSandwichDto { get; } = new NewSandwichDto();

        protected async Task Add()
        {
            NewSandwichDto.Id = 0;

            var result = await SandwichService.AddOrUpdateSandwichAsync(NewSandwichDto);

            switch (result)
            {
                case Success _:
                    UriHelper.NavigateTo(Urls.Client.GetAllSandwich);
                    break;
            }
        }
    }
}