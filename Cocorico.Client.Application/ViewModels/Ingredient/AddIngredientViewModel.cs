using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Ingredient;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.Ingredient
{
    public class AddIngredientViewModel : IAddIngredientViewModel
    {
        private readonly IIngredientClient _ingredientClient;

        public IngredientAddDto IngredientAddDto { get; } = new IngredientAddDto();

        public AddIngredientViewModel(IIngredientClient ingredientClient) => _ingredientClient = ingredientClient;

        public async Task<bool> AddAsync()
        {
            var result = await _ingredientClient.AddAsync(IngredientAddDto);

            return result.IsSuccessfulStatusCode();
        }
    }
}