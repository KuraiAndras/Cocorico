using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Sandwich
{
    public interface IAddSandwichViewModel
    {
        SandwichAddDto NewSandwichDto { get; }
        List<IngredientDto> AvailableIngredients { get; }
        List<IngredientDto> AddedIngredients { get; }

        Task LoadAvailableIngredientsAsync();
        void AddIngredient(IngredientDto ingredient);
        void RemoveIngredient(IngredientDto ingredient);
        Task AddAsync();
    }
}
