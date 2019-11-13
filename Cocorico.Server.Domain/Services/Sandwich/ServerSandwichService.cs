﻿using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Models.Entities;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.Sandwich
{
    public class ServerSandwichService : EntityServiceBase<Models.Entities.Sandwich>, IServerSandwichService
    {
        public ServerSandwichService(CocoricoDbContext context) : base(context)
        {
        }

        public async Task<SandwichDto> GetAsync(int id)
        {
            var sandwich = await Context
                               .Sandwiches
                               .Include(s => s.IngredientLinks)
                               .ThenInclude(il => il.Ingredient)
                               .SingleOrDefaultAsync(s => s.Id == id)
                           ?? throw new EntityNotFoundException($"Cant find sandwich with id:{id}");

            return sandwich.ToSandwichDto();
        }

        public async Task<IEnumerable<SandwichDto>> GetAllAsync()
        {
            var sandwiches = await Context
                .Sandwiches
                .Include(s => s.IngredientLinks)
                .ThenInclude(il => il.Ingredient)
                .ToListAsync();

            return sandwiches.Select(s => s.ToSandwichDto());
        }

        public async Task AddAsync(SandwichAddDto sandwichAddDto)
        {
            var sandwich = sandwichAddDto.ToSandwich();

            var ingredients = await Context
                .Ingredients
                .ToListAsync();

            sandwich.IngredientLinks = ingredients
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
            var original = await Context
                .Sandwiches
                .AsNoTracking()
                .Include(s => s.IngredientLinks)
                .ThenInclude(il => il.Ingredient)
                .SingleAsync(s => s.Id == sandwichDto.Id);

            var ingredientsToAdd = sandwichDto
                .Ingredients
                .Except(original.Ingredients().Select(i => i.ToIngredientDto()))
                .Select(i => new SandwichIngredient
                {
                    SandwichId = sandwichDto.Id,
                    IngredientId = i.Id,
                });

            await Context.Set<SandwichIngredient>().AddRangeAsync(ingredientsToAdd);

            var ingredientsToRemove = original
                .Ingredients()
                .Except(sandwichDto.Ingredients.Select(i => i.ToIngredient()))
                .Select(i => new SandwichIngredient
                {
                    SandwichId = sandwichDto.Id,
                    IngredientId = i.Id,
                });

            Context.Set<SandwichIngredient>().RemoveRange(ingredientsToRemove);

            await Context.SaveChangesAsync();

            await UpdateAsync(sandwichDto.ToSandwich());
        }

        public async Task DeleteAsync(int id) =>
            await DeleteByIdAsync(id);
    }
}