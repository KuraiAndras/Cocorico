using Cocorico.Shared.Api.Ingredients;
using Cocorico.Shared.Api.Orders;
using Cocorico.Shared.Api.Sandwiches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Order
{
    public interface IAddCustomerOrderViewModel
    {
        AddOrder AddOrder { get; }
        List<IngredientDto> AvailableIngredients { get; }

        Task AddAsync();
        Task EditAsync(SandwichDto sandwich);
        void DeleteSandwich(int id);
        Task LoadIngredientsAsync();

        void AddIngredient(IngredientDto ingredient);
        void RemoveIngredient(IngredientDto ingredient);
    }
}
