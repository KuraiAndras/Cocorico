using Cocorico.Client.Application.Services.Basket;
using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.Sandwich
{
    public class SandwichesViewModel : ISandwichesViewModel
    {
        private readonly IBasketService _basketService;
        private readonly ISandwichClient _sandwichClient;

        public SandwichesViewModel(
            IBasketService basketService,
            ISandwichClient sandwichClient)
        {
            _basketService = basketService;
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
            }
        }

        public async Task DeleteAsync(int sandwichId)
        {
            try
            {
                var fileResponse = await _sandwichClient.DeleteAsync(sandwichId);

                if (fileResponse.IsSuccessfulStatusCode()) await LoadSandwichesAsync();
            }
            catch (SwaggerException)
            {
            }
        }

        public void AddToBasket(SandwichDto sandwich) => _basketService.AddToBasket(sandwich);
    }
}
