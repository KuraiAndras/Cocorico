using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Ingredient;

namespace Cocorico.Client.Application.ViewModels.Ingredient
{
    public interface IAddIngredientViewModel
    {
        IngredientAddDto IngredientAddDto { get; }
        Task AddAsync();
    }
}
