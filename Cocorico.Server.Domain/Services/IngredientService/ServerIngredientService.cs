using Cocorico.DAL.Models;
using Cocorico.DAL.Models.Entities;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.IngredientService
{
    public class ServerIngredientService : EntityServiceBase<Ingredient>, IServerIngredientService
    {
        public ServerIngredientService(CocoricoDbContext context) : base(context)
        {
        }

        public async Task<ICollection<IngredientDto>> GetAllAsync() =>
            (await Context.Ingredients.ToListAsync()
             ?? throw new UnexpectedException())
            .Select(i => i.MapTo<Ingredient, IngredientDto>())
            .ToList();

        public async Task<IngredientDto> GetAsync(int id) =>
            (await Context.Ingredients.SingleOrDefaultAsync(i => i.Id == id)
             ?? throw new EntityNotFoundException())
            .MapTo<Ingredient, IngredientDto>();

        public async Task AddAsync(IngredientAddDto ingredientAddDto) =>
            await AddAsync(ingredientAddDto.MapTo<IngredientAddDto, Ingredient>());

        public async Task UpdateAsync(IngredientDto ingredientDto) =>
            await UpdateAsync(ingredientDto.ToIngredient());

        public async Task DeleteAsync(int id) =>
            await DeleteByIdAsync(id);
    }
}
