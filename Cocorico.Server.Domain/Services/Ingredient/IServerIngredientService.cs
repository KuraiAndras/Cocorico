﻿using Cocorico.Shared.Dtos.Ingredient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.Ingredient
{
    public interface IServerIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetAllAsync();
        Task<IngredientDto> GetAsync(int id);
        Task AddAsync(IngredientAddDto ingredientAddDto);
        Task UpdateAsync(IngredientDto ingredientDto);
        Task DeleteAsync(int id);
    }
}
