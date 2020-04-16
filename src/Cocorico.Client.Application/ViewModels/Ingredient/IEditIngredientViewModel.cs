using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Ingredients;

namespace Cocorico.Client.Application.ViewModels.Ingredient
{
    public interface IEditIngredientViewModel
    {
        IngredientDto IngredientDto { get; }

        Task LoadIngredientAsync(int id);
        Task<bool> EditIngredientAsync();
    }
}
