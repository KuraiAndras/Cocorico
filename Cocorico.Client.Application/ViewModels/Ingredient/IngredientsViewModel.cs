using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Ingredient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.Ingredient
{
    public class IngredientsViewModel : IIngredientsViewModel
    {
        private readonly IIngredientClient _ingredientClient;

        public IReadOnlyList<IngredientDto> IngredientsList { get; private set; }

        public IngredientsViewModel(IIngredientClient ingredientClient)
        {
            _ingredientClient = ingredientClient;
            IngredientsList = new List<IngredientDto>();
        }

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
