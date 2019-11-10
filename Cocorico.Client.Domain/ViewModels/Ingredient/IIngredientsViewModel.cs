using Cocorico.Shared.Dtos.Ingredient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Ingredient
{
    public interface IIngredientsViewModel
    {
        IReadOnlyList<IngredientDto> IngredientsList { get; }

        void GoToEdit(int id);
        Task DeleteAsync(int id);
        Task LoadIngredientsAsync();
    }
}
