using Cocorico.Shared.Api.Ingredients;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Ingredient
{
    public interface IAddIngredientViewModel
    {
        AddIngredient AddIngredient { get; }
        Task<bool> AddAsync();
    }
}
