using Cocorico.HttpClient;
using Cocorico.Client.Domain.Services.Basket;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocorico.HttpClient.Extensions;

namespace Cocorico.Client.Domain.ViewModels.Sandwich
{
    public class SandwichesViewModel : ISandwichesViewModel
    {
        private readonly IBasketService _basketService;
        private readonly NavigationManager _navigationManager;
        private readonly ISandwichClient _sandwichClient;

        public SandwichesViewModel(
            IBasketService basketService,
            NavigationManager navigationManager,
            ISandwichClient sandwichClient)
        {
            _basketService = basketService;
            _navigationManager = navigationManager;
            _sandwichClient = sandwichClient;
            SandwichesList = new List<SandwichDto>();
        }

        public IReadOnlyList<SandwichDto> SandwichesList { get; private set; }

        public async Task LoadSandwichesAsync()
        {
            try
            {
                var sandwiches = await _sandwichClient.GetAllAsync();

                SandwichesList = sandwiches.ToList();
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        public void Edit(int sandwichId) => _navigationManager.NavigateTo(Urls.Client.Sandwiches + $"/{sandwichId}");

        public async Task DeleteAsync(int sandwichId)
        {
            try
            {
                var fileResponse = await _sandwichClient.DeleteAsync(sandwichId);

                if (fileResponse.IsSuccessfulStatusCode()) await LoadSandwichesAsync();
                //TODO: Handle fail
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        public void AddToBasket(SandwichDto sandwich) => _basketService.AddToBasket(sandwich);
    }
}