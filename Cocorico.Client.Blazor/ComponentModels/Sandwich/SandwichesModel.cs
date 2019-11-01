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
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private ICocoricoClientAuthenticationService AuthenticationService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private NavigationManager UriHelper { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private ISandwichClient SandwichHttpClient { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IBasketService BasketService { get; set; }

        protected bool IsAdmin => AuthenticationService.Claims.Contains(Claims.Admin);

        protected bool IsCustomer => AuthenticationService.Claims.Contains(Claims.Customer);

        protected IReadOnlyList<SandwichDto> Sandwiches { get; private set; } = new List<SandwichDto>();

        protected override async Task OnInitializedAsync() => await LoadSandwichesAsync();

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

        protected void Edit(int sandwichId) => UriHelper.NavigateTo(Urls.Client.Sandwiches + $"/{sandwichId}");

        protected async Task DeleteAsync(int sandwichId)
        {
            try
            {
                var fileResponse = await SandwichHttpClient.DeleteAsync(sandwichId);

                if (fileResponse.IsSuccessfulStatusCode()) await LoadSandwichesAsync();
                //TODO: Handle fail
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        protected void AddToBasket(SandwichDto sandwich) => BasketService.AddToBasket(sandwich);
    }
}
