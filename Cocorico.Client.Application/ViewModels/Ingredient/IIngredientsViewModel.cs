using System.Collections.Generic;
using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Ingredient;

namespace Cocorico.Client.Application.ViewModels.Ingredient
{
    public interface IIngredientsViewModel
    {
        IReadOnlyList<IngredientDto> IngredientsList { get; }

        void GoToEdit(int id);
        Task DeleteAsync(int id);
        Task LoadIngredientsAsync();
    }
}
