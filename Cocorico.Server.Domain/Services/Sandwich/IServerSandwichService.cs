﻿using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.Sandwich
{
    public interface IServerSandwichService
    {
        Task<SandwichDto> GetAsync(int id);
        Task<IEnumerable<SandwichDto>> GetAllAsync();
        Task AddAsync(SandwichAddDto sandwichAddDto);
        Task UpdateAsync(SandwichDto sandwichDto);
        Task DeleteAsync(int id);
    }
}
