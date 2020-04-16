using Cocorico.Shared.Dtos.Ingredients;
using Cocorico.Shared.Dtos.Orders;
using Cocorico.Shared.Dtos.Sandwiches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Order
{
    public interface IAddCustomerOrderViewModel
    {
        AddOrderDto AddOrderDto { get; }
        List<IngredientDto> AvailableIngredients { get; }

        Task AddAsync();
        Task EditAsync(SandwichDto sandwich);
        void DeleteSandwich(int id);
        Task LoadIngredientsAsync();

        void AddIngredient(IngredientDto ingredient);
        void RemoveIngredient(IngredientDto ingredient);
    }
}
