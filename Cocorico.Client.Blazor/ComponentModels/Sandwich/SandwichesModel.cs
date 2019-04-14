using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Client.Domain.Services.Sandwich;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;

namespace Cocorico.Client.Blazor.ComponentModels.Sandwich
{
    public class SandwichesModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] protected ICocoricoClientAuthenticationService CocoricoClientAuthenticationService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IClientSandwichService SandwichService { get; set; }

        protected IReadOnlyList<SandwichResultDto> Sandwiches { get; private set; } = new List<SandwichResultDto>();

        protected override async Task OnInitAsync() => await LoadSandwichesAsync();

        private async Task LoadSandwichesAsync()
        {
            var result = await SandwichService.GetAllSandwichResultAsync();

            //TODO: Handle fail
            switch (result)
            {
                case Success<IEnumerable<SandwichResultDto>> success:
                    Sandwiches = success.Data.ToList();
                    break;
            }
        }

        protected void Edit(int sandwichId) => UriHelper.NavigateTo(Urls.Client.EditSandwich + $"/{sandwichId}");

        protected async Task DeleteAsync(int sandwichId)
        {
            var result = await SandwichService.DeleteSandwichAsync(sandwichId);

            if (result is Success) await LoadSandwichesAsync();
        }
    }
}
