using Cocorico.Shared.Dtos.Ingredient;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Ingredient
{
    public interface IEditIngredientViewModel
    {
        IngredientDto IngredientDto { get; }

        Task LoadIngredientAsync(int id);
        Task EditIngredientAsync();
    }
}
