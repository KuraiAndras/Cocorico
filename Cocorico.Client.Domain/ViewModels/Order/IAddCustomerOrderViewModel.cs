using Cocorico.Shared.Dtos.Order;
using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Ingredient;
using System.Collections.Generic;
using Cocorico.Shared.Dtos.Sandwich;

namespace Cocorico.Client.Domain.ViewModels.Order
{
    public interface IAddCustomerOrderViewModel
    {
        AddOrderDto AddOrderDto { get; }

        Task AddAsync();
        Task EditAsync(SandwichDto sandwich);
        void DeleteSandwich(int id);
        Task LoadIngredientsAsync();
        List<IngredientDto> AvailableIngredients { get; }
        void AddIngredient(IngredientDto ingredient);
        void RemoveIngredient(IngredientDto ingredient);
    }
}
