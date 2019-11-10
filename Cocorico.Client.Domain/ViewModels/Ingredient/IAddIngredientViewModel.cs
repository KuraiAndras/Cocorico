using Cocorico.Shared.Dtos.Ingredient;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Ingredient
{
    public interface IAddIngredientViewModel
    {
        IngredientAddDto IngredientAddDto { get; }
        Task AddAsync();
    }
}
