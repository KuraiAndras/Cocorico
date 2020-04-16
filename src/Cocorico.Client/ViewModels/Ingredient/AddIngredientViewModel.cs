using Cocorico.Client.Extensions;
using Cocorico.Client.HttpClient;
using Cocorico.Shared.Dtos.Ingredients;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Ingredient
{
    public class AddIngredientViewModel : IAddIngredientViewModel
    {
        private readonly IIngredientClient _ingredientClient;

        public AddIngredientViewModel(IIngredientClient ingredientClient) => _ingredientClient = ingredientClient;

        public IngredientAddDto IngredientAddDto { get; } = new IngredientAddDto();

        public async Task<bool> AddAsync()
        {
            var result = await _ingredientClient.AddAsync(IngredientAddDto);

            var isSuccesful = result.IsSuccessfulStatusCode();

            result.Dispose();

            return isSuccesful;
        }
    }
}
