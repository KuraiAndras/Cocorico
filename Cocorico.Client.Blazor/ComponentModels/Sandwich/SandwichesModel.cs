using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Client.Domain.Services.Basket;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Sandwich
{
    public class SandwichesModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] protected ICocoricoClientAuthenticationService CocoricoClientAuthenticationService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private ISandwichClient SandwichHttpClient { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IBasketService BasketService { get; set; }

        protected IReadOnlyList<SandwichResultDto> Sandwiches { get; private set; } = new List<SandwichResultDto>();

        protected override async Task OnInitAsync() => await LoadSandwichesAsync();

        private async Task LoadSandwichesAsync()
        {
            try
            {
                var sandwiches = await SandwichHttpClient.GetAllAsync();

                Sandwiches = sandwiches.ToList();
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        protected void Edit(int sandwichId) => UriHelper.NavigateTo(Urls.Client.EditSandwich + $"/{sandwichId}");

        protected async Task DeleteAsync(int sandwichId)
        {
            try
            {
                var fileResponse = await SandwichHttpClient.DeleteAsync(sandwichId);

                if (fileResponse.IsSuccessfulStatusCode()) await LoadSandwichesAsync();
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        protected void AddToBasket(SandwichResultDto sandwich) => BasketService.AddToBasket(sandwich);
    }
}
