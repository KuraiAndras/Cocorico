using AutoMapper;
using Cocorico.Domain.Entities;
using Cocorico.Domain.Exceptions;
using Cocorico.Persistence;
using Cocorico.Server.Domain.Services.Opening;
using Cocorico.Shared.Dtos.Sandwich;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.SandwichService
{
    public class ServerSandwichService : IServerSandwichService
    {
        private readonly CocoricoDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOpeningService _openingService;

        public ServerSandwichService(
            CocoricoDbContext context,
            IMapper mapper,
            IOpeningService openingService)
        {
            _context = context;
            _mapper = mapper;
            _openingService = openingService;
        }

        public async Task<SandwichDto> GetAsync(int id)
        {
            var sandwich = await _context
                               .Sandwiches
                               .Include(s => s.SandwichIngredients)
                               .ThenInclude(il => il.Ingredient)
                               .SingleOrDefaultAsync(s => s.Id == id)
                           ?? throw new EntityNotFoundException($"Cant find sandwich with id:{id}");

            return _mapper.Map<SandwichDto>(sandwich);
        }

        public async Task<ICollection<SandwichDto>> GetAllAsync()
        {
            var sandwiches = await _context
                .Sandwiches
                .Include(s => s.SandwichIngredients)
                .ThenInclude(il => il.Ingredient)
                .ToListAsync();

            return sandwiches.Select(s => _mapper.Map<SandwichDto>(s)).ToList();
        }

        public async Task AddAsync(SandwichAddDto sandwichAddDto)
        {
            var sandwich = _mapper.Map<Sandwich>(sandwichAddDto);

            var ingredients = await _context
                .Ingredients
                .ToListAsync();

            sandwich.SandwichIngredients = ingredients
                .Where(i => sandwichAddDto.Ingredients.Any(iDto => iDto.Id == i.Id))
                .Select(i => new SandwichIngredient
                {
                    Ingredient = i,
                    Sandwich = sandwich,
                })
                .ToList();

            _context.Sandwiches.Add(sandwich);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SandwichDto sandwichDto)
        {
            var dateAdded = DateTime.Now;
            if (!await _openingService.CanAddOrderAsync(dateAdded))
                throw new StoreClosedException();

            var updatedSandwich = _mapper.Map<Sandwich>(sandwichDto);

            var originalSandwich = await _context.Sandwiches
                .AsNoTracking()
                .Include(s => s.SandwichIngredients)
                .SingleOrDefaultAsync(s => s.Id == sandwichDto.Id);

            if (originalSandwich is null) throw new EntityNotFoundException();

            updatedSandwich.SandwichIngredients = originalSandwich.SandwichIngredients
                .Where(si => sandwichDto.Ingredients.Any(i => si.SandwichId == i.Id))
                .ToList();

            var ingredientsInDb = await _context.Ingredients.ToListAsync();

            foreach (var ingredientDto in sandwichDto.Ingredients
                .Where(ingredientDto => updatedSandwich.SandwichIngredients.All(si => si.IngredientId != ingredientDto.Id)))
            {
                updatedSandwich.SandwichIngredients.Add(new SandwichIngredient
                {
                    Sandwich = updatedSandwich,
                    Ingredient = ingredientsInDb.Single(i => i.Id == ingredientDto.Id),
                });
            }

            _context.Sandwiches.Update(updatedSandwich);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sandwichToDelete = await _context.Sandwiches.SingleOrDefaultAsync(s => s.Id.Equals(id));

            _context.Remove(sandwichToDelete);

            await _context.SaveChangesAsync();
        }
    }
}