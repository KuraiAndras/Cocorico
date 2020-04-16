using Cocorico.Shared.Dtos.Ingredients;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Ingredient
{
    public interface IEditIngredientViewModel
    {
        IngredientDto IngredientDto { get; }

        Task LoadIngredientAsync(int id);
        Task<bool> EditIngredientAsync();
    }
}
