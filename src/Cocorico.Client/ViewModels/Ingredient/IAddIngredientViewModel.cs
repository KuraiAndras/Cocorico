using Cocorico.Shared.Dtos.Ingredients;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Ingredient
{
    public interface IAddIngredientViewModel
    {
        IngredientAddDto IngredientAddDto { get; }
        Task<bool> AddAsync();
    }
}
