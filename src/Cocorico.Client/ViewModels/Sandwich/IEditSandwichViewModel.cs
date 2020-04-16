using Cocorico.Shared.Dtos.Ingredients;
using Cocorico.Shared.Dtos.Sandwiches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Sandwich
{
    public interface IEditSandwichViewModel
    {
        SandwichDto Sandwich { get; }

        List<IngredientDto> AvailableIngredients { get; }
        void AddIngredient(IngredientDto ingredient);
        Task<bool> TryEditAsync();
        void RemoveIngredient(IngredientDto ingredient);
        Task LoadIngredientsAsync(int id);
    }
}
