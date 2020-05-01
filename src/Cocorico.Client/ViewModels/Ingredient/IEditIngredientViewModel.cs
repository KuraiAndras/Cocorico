using Cocorico.Shared.Api.Ingredients;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Ingredient
{
    public interface IEditIngredientViewModel
    {
        UpdateIngredient IngredientDto { get; }

        Task LoadIngredientAsync(int id);
        Task<bool> EditIngredientAsync();
    }
}
