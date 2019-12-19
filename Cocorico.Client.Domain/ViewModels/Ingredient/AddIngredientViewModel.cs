using Cocorico.HttpClient;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Cocorico.HttpClient.Extensions;

namespace Cocorico.Client.Domain.ViewModels.Ingredient
{
    public class AddIngredientViewModel : IAddIngredientViewModel
    {
        private readonly IIngredientClient _ingredientClient;
        private readonly NavigationManager _navigationManager;

        public IngredientAddDto IngredientAddDto { get; }

        public AddIngredientViewModel(IIngredientClient ingredientClient, NavigationManager navigationManager)
        {
            _ingredientClient = ingredientClient;
            _navigationManager = navigationManager;
            IngredientAddDto = new IngredientAddDto();
        }

        public async Task AddAsync()
        {
            var result = await _ingredientClient.AddAsync(IngredientAddDto);

            if (result.IsSuccessfulStatusCode()) _navigationManager.NavigateTo(Urls.Client.Ingredients);
        }
    }
}