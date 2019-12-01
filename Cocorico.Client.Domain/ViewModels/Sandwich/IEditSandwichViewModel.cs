using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Sandwich
{
    public interface IEditSandwichViewModel
    {
        SandwichDto Sandwich { get; }

        List<IngredientDto> AvailableIngredients { get; }
        void AddIngredient(IngredientDto ingredient);
        Task EditAsync();
        void RemoveIngredient(IngredientDto ingredient);
        Task LoadIngredientsAsync(int id);
    }
}
