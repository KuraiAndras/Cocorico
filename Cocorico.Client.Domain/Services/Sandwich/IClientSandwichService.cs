﻿using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.Services.Sandwich
{
    public interface IClientSandwichService
    {
        Task<IServiceResult<SandwichResultDto>> GetSandwichResultAsync(int id);
        Task<IServiceResult<IEnumerable<SandwichResultDto>>> GetAllSandwichResultAsync();
        Task<IServiceResult> AddOrUpdateSandwichAsync(NewSandwichDto newSandwichDto);
        Task<IServiceResult> DeleteSandwichAsync(int id);
    }
}
