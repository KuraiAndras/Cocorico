using AutoMapper;
using Cocorico.Domain.Entities;
using Cocorico.Domain.Exceptions;
using Cocorico.Persistence;
using Cocorico.Shared.Dtos.Ingredient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.IngredientService
{
    public class ServerIngredientService : IServerIngredientService
    {
        private readonly CocoricoDbContext _context;
        private readonly IMapper _mapper;

        public ServerIngredientService(
            CocoricoDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<IngredientDto>> GetAllAsync() =>
            (await _context.Ingredients.ToListAsync()
             ?? throw new UnexpectedException())
            .Select(i => _mapper.Map<IngredientDto>(i))
            .ToList();

        public async Task<IngredientDto> GetAsync(int id)
        {
            var result = await _context.Ingredients.SingleOrDefaultAsync(i => i.Id == id) ?? throw new EntityNotFoundException();
            return _mapper.Map<IngredientDto>(result);
        }

        public async Task AddAsync(IngredientAddDto ingredientAddDto)
        {
            var ingredientToAdd = _mapper.Map<Ingredient>(ingredientAddDto);

            await _context.Ingredients.AddAsync(ingredientToAdd);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IngredientDto ingredientDto)
        {
            var ingredientToUpdate = _mapper.Map<Ingredient>(ingredientDto);

            _context.Ingredients.Update(ingredientToUpdate);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ingredientToRemove = await _context.Ingredients.SingleOrDefaultAsync(i => i.Id.Equals(id));

            if (ingredientToRemove is null) throw new EntityNotFoundException();

            _context.Ingredients.Remove(ingredientToRemove);

            await _context.SaveChangesAsync();
        }
    }
}
