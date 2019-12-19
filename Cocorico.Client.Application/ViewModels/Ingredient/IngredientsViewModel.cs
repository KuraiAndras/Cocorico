using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;

namespace Cocorico.Client.Application.ViewModels.Ingredient
{
    public class IngredientsViewModel : IIngredientsViewModel
    {
        private readonly IIngredientClient _ingredientClient;
        private readonly NavigationManager _navigationManager;

        public IReadOnlyList<IngredientDto> IngredientsList { get; private set; }

        public IngredientsViewModel(IIngredientClient ingredientClient, NavigationManager navigationManager)
        {
            _ingredientClient = ingredientClient;
            _navigationManager = navigationManager;
            IngredientsList = new List<IngredientDto>();
        }

        public void GoToEdit(int id) => _navigationManager.NavigateTo(Urls.Client.Ingredients + $"/{id}");

        public async Task DeleteAsync(int id)
        {
            try
            {
                var fileResponse = await _ingredientClient.DeleteAsync(id);

                if (fileResponse.IsSuccessfulStatusCode()) await LoadIngredientsAsync();
                //TODO: Handle fail
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        public async Task LoadIngredientsAsync()
        {
            try
            {
                var ingredients = await _ingredientClient.GetAllAsync();

                IngredientsList = ingredients.ToList();
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}