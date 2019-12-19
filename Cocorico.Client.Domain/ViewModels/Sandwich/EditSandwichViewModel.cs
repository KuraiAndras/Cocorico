using Cocorico.Client.Domain.Extensions;
using Cocorico.HttpClient;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Sandwich
{
    public class EditSandwichViewModel : IEditSandwichViewModel
    {
        private readonly NavigationManager _navigationManager;
        private readonly ISandwichClient _sandwichClient;
        private readonly IIngredientClient _ingredientClient;
        private readonly List<IngredientDto> _addedIngredients;

        public EditSandwichViewModel(
            NavigationManager navigationManager,
            ISandwichClient sandwichClient,
            IIngredientClient ingredientClient)
        {
            _navigationManager = navigationManager;
            _sandwichClient = sandwichClient;
            _ingredientClient = ingredientClient;

            _addedIngredients = new List<IngredientDto>();

            Sandwich = new SandwichDto();
            AvailableIngredients = new List<IngredientDto>();
        }

        public SandwichDto Sandwich { get; private set; }
        public List<IngredientDto> AvailableIngredients { get; }


        public async Task LoadIngredientsAsync(int id)
        {
            try
            {
                var sandwichDto = await _sandwichClient.GetAsync(id);
                Sandwich = sandwichDto;
                _addedIngredients.Clear();
                _addedIngredients.AddRange(sandwichDto.Ingredients);

                var ingredients = await _ingredientClient.GetAllAsync();

                AvailableIngredients.AddRange(ingredients);
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        public void AddIngredient(IngredientDto ingredient)
        {
            _addedIngredients.Add(ingredient);
            Sandwich.Ingredients = _addedIngredients;
        }

        public void RemoveIngredient(IngredientDto ingredient)
        {
            var ingredientToRemove = _addedIngredients.Single(i => i.Id == ingredient.Id);
            _addedIngredients.Remove(ingredientToRemove);
            Sandwich.Ingredients = _addedIngredients;
        }

        public async Task EditAsync()
        {
            try
            {
                Sandwich.Ingredients = _addedIngredients;

                var fileResponse = await _sandwichClient.UpdateAsync(Sandwich);

                //TODO: Handle fail
                if (!fileResponse.IsSuccessfulStatusCode()) return;

                _navigationManager.NavigateTo(Urls.Client.Sandwiches);
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}