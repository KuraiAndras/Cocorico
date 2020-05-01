using Cocorico.Shared.Api.Ingredients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Ingredient
{
    public interface IIngredientsViewModel
    {
        IReadOnlyList<IngredientDto> IngredientsList { get; }
        Task DeleteAsync(int id);
        Task LoadIngredientsAsync();
    }
}
