using Cocorico.Shared.Api.Ingredients;
using Cocorico.Shared.Api.Sandwiches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Sandwich
{
    public interface IEditSandwichViewModel
    {
        UpdateSandwich Sandwich { get; }

        List<IngredientDto> AvailableIngredients { get; }
        void AddIngredient(IngredientDto ingredient);
        Task<bool> TryEditAsync();
        void RemoveIngredient(IngredientDto ingredient);
        Task LoadIngredientsAsync(int id);
    }
}
