using Cocorico.Shared.Dtos.Ingredient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.IngredientService
{
    public interface IServerIngredientService
    {
        Task<ICollection<IngredientDto>> GetAllAsync();
        Task<IngredientDto> GetAsync(int id);
        Task AddAsync(IngredientAddDto ingredientAddDto);
        Task UpdateAsync(IngredientDto ingredientDto);
        Task DeleteAsync(int id);
    }
}
