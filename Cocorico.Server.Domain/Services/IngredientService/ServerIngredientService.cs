using AutoMapper;
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
        private readonly IMapper _mapper;

        public ServerIngredientService(CocoricoDbContext context, IMapper mapper) : base(context) =>
            _mapper = mapper;

        public async Task<ICollection<IngredientDto>> GetAllAsync() =>
            (await Context.Ingredients.ToListAsync()
             ?? throw new UnexpectedException())
            .Select(i => _mapper.Map<IngredientDto>(i))
            .ToList();

        public async Task<IngredientDto> GetAsync(int id)
        {
            var result = await Context.Ingredients.SingleOrDefaultAsync(i => i.Id == id) ?? throw new EntityNotFoundException();
            return _mapper.Map<IngredientDto>(result);
        }

        public async Task AddAsync(IngredientAddDto ingredientAddDto) =>
            await AddAsync(_mapper.Map<Ingredient>(ingredientAddDto));

        public async Task UpdateAsync(IngredientDto ingredientDto) =>
            await UpdateAsync(_mapper.Map<Ingredient>(ingredientDto));

        public async Task DeleteAsync(int id) =>
            await DeleteByIdAsync(id);
    }
}
