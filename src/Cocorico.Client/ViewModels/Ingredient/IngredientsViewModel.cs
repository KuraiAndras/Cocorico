using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Ingredients;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Ingredient
{
    public class IngredientsViewModel : IIngredientsViewModel
    {
        private readonly IIngredientClient _ingredientClient;

        public IngredientsViewModel(IIngredientClient ingredientClient)
        {
            _ingredientClient = ingredientClient;
            IngredientsList = new List<IngredientDto>();
        }

        public IReadOnlyList<IngredientDto> IngredientsList { get; private set; }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var fileResponse = await _ingredientClient.DeleteAsync(id);

                if (fileResponse.IsSuccessfulStatusCode()) await LoadIngredientsAsync();
            }
            catch (SwaggerException)
            {
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
            }
        }
    }
}
