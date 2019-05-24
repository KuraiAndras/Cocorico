using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocorico.Server.Domain.Models.Entities;

namespace Cocorico.Server.Domain.Services.Ingredient
{
    public class ServerIngredientService : EntityServiceBase<Models.Entities.Ingredient>, IServerIngredientService
    {
        public ServerIngredientService(CocoricoDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<IngredientDto>> GetAllAsync() =>
            (await Context.Ingredients.ToListAsync()
             ?? throw new UnexpectedException())
            .Select(i => i.MapTo<Models.Entities.Ingredient, IngredientDto>());

        public async Task<IngredientDto> GetAsync(int id) =>
            (await Context.Ingredients.SingleOrDefaultAsync(i => i.Id == id)
             ?? throw new EntityNotFoundException())
            .MapTo<Models.Entities.Ingredient, IngredientDto>();

        public async Task AddAsync(IngredientAddDto ingredientAddDto) =>
            await AddAsync(ingredientAddDto.MapTo<IngredientAddDto, Models.Entities.Ingredient>());

        public async Task UpdateAsync(IngredientDto ingredientDto) =>
            await UpdateAsync(ingredientDto.ToIngredient());

        public async Task DeleteAsync(int id) =>
            await DeleteByIdAsync(id);
    }
}
