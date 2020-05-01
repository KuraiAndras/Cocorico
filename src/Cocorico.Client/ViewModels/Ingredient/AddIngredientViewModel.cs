using Cocorico.Client.Extensions;
using Cocorico.Client.HttpClient;
using Cocorico.Shared.Api.Ingredients;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Ingredient
{
    public class AddIngredientViewModel : IAddIngredientViewModel
    {
        private readonly IIngredientClient _ingredientClient;

        public AddIngredientViewModel(IIngredientClient ingredientClient) => _ingredientClient = ingredientClient;

        public AddIngredient AddIngredient { get; } = new AddIngredient();

        public async Task<bool> AddAsync()
        {
            var result = await _ingredientClient.AddAsync(AddIngredient);

            var isSuccesful = result.IsSuccessfulStatusCode();

            result.Dispose();

            return isSuccesful;
        }
    }
}
