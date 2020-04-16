using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Ingredients;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.Ingredient
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
