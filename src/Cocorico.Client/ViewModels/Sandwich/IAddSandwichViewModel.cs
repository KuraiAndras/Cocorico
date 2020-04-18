using Cocorico.Shared.Api.Ingredients;
using Cocorico.Shared.Api.Sandwiches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Sandwich
{
    public interface IAddSandwichViewModel
    {
        AddSandwich NewAddSandwichDto { get; }
        List<IngredientDto> AvailableIngredients { get; }
        List<IngredientDto> AddedIngredients { get; }

        Task LoadAvailableIngredientsAsync();
        void AddIngredient(IngredientDto ingredient);
        void RemoveIngredient(IngredientDto ingredient);
        Task<bool> TryAddAsync();
    }
}
