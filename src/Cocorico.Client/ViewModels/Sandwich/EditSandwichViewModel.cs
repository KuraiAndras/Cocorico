using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Ingredients;
using Cocorico.Shared.Dtos.Sandwiches;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Sandwich
{
    public class EditSandwichViewModel : IEditSandwichViewModel
    {
        private readonly ISandwichClient _sandwichClient;
        private readonly IIngredientClient _ingredientClient;
        private readonly List<IngredientDto> _addedIngredients;

        public EditSandwichViewModel(
            ISandwichClient sandwichClient,
            IIngredientClient ingredientClient)
        {
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

        public async Task<bool> TryEditAsync()
        {
            try
            {
                Sandwich.Ingredients = _addedIngredients;

                var fileResponse = await _sandwichClient.UpdateAsync(Sandwich);

                return fileResponse.IsSuccessfulStatusCode();
            }
            catch (SwaggerException)
            {
                return false;
            }
        }
    }
}
