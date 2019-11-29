using AutoMapper;
using Cocorico.DAL.Models;
using Cocorico.DAL.Models.Entities;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.SandwichService
{
    public class ServerSandwichService : EntityServiceBase<Sandwich>, IServerSandwichService
    {
        private readonly IMapper _mapper;

        public ServerSandwichService(CocoricoDbContext context, IMapper mapper) : base(context) =>
            _mapper = mapper;

        public async Task<SandwichDto> GetAsync(int id)
        {
            var sandwich = await Context
                               .Sandwiches
                               .Include(s => s.SandwichIngredients)
                               .ThenInclude(il => il.Ingredient)
                               .SingleOrDefaultAsync(s => s.Id == id)
                           ?? throw new EntityNotFoundException($"Cant find sandwich with id:{id}");

            return _mapper.Map<SandwichDto>(sandwich);
        }

        public async Task<ICollection<SandwichDto>> GetAllAsync()
        {
            var sandwiches = await Context
                .Sandwiches
                .Include(s => s.SandwichIngredients)
                .ThenInclude(il => il.Ingredient)
                .ToListAsync();

            return sandwiches.Select(s => _mapper.Map<SandwichDto>(s)).ToList();
        }

        public async Task AddAsync(SandwichAddDto sandwichAddDto)
        {
            var sandwich = _mapper.Map<Sandwich>(sandwichAddDto);

            var ingredients = await Context
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

            await AddAsync(sandwich);
        }

        public async Task UpdateAsync(SandwichDto sandwichDto)
        {
            var updatedSandwich = _mapper.Map<Sandwich>(sandwichDto);

            var originalSandwich = await Context.Sandwiches
                .AsNoTracking()
                .Include(s => s.SandwichIngredients)
                .SingleOrDefaultAsync(s => s.Id == sandwichDto.Id);

            if (originalSandwich is null) throw new EntityNotFoundException();

            updatedSandwich.SandwichIngredients = originalSandwich.SandwichIngredients
                .Where(si => sandwichDto.Ingredients.Any(i => si.SandwichId == i.Id))
                .ToList();

            var ingredientsInDb = await Context.Ingredients.ToListAsync();

            foreach (var ingredientDto in sandwichDto.Ingredients
                .Where(ingredientDto => updatedSandwich.SandwichIngredients.All(si => si.IngredientId != ingredientDto.Id)))
            {
                updatedSandwich.SandwichIngredients.Add(new SandwichIngredient
                {
                    Sandwich = updatedSandwich,
                    Ingredient = ingredientsInDb.Single(i => i.Id == ingredientDto.Id),
                });
            }

            Context.Sandwiches.Update(updatedSandwich);

            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) =>
            await DeleteByIdAsync(id);
    }
}