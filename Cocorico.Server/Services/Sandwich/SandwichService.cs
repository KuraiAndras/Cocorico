using System;
using Cocorico.Server.Extensions;
using Cocorico.Server.Models;
using Cocorico.Shared.Dtos.Sandwich;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocorico.Server.Helpers;
using Cocorico.Shared.Helpers;

namespace Cocorico.Server.Services.Sandwich
{
    public class SandwichService : ISandwichService
    {
        private readonly CocoricoDbContext _cocoricoDbContext;

        public SandwichService(CocoricoDbContext cocoricoDbContext) => _cocoricoDbContext = cocoricoDbContext;

        public async Task<ServiceResult<SandwichResultDto>> GetAsync(int id)
        {
            var sandwich = await _cocoricoDbContext.Sandwiches.FirstOrDefaultAsync(s => s.Id == id);

            return sandwich is null
                ? new ServiceResult<SandwichResultDto>(new InvalidOperationException())
                : new ServiceResult<SandwichResultDto>(sandwich.ToSandwichResultDto());
        }

        public async Task<ServiceResult<IEnumerable<SandwichResultDto>>> GetAllAsync()
        {
            var sandwiches = await _cocoricoDbContext
                .Sandwiches
                .Select(s => s.ToSandwichResultDto())
                .ToListAsync();

            return new ServiceResult<IEnumerable<SandwichResultDto>>(sandwiches);
        }

        public async Task<ServiceResult> AddOrUpdateAsync(NewSandwichDto newSandwichDto)
        {
            var sandwich = newSandwichDto.ToSandwich();

            var set = _cocoricoDbContext.Sandwiches;

            var original = await set.SingleOrDefaultAsync(s => s.Id == sandwich.Id);

            if (!(original is null)) set.Remove(original);

            await set.AddAsync(sandwich);

            await _cocoricoDbContext.SaveChangesAsync();

            return new ServiceResult();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var set = _cocoricoDbContext.Sandwiches;

            var original = await set.SingleOrDefaultAsync(s => s.Id == id);

            if (original is null) return new ServiceResult(new InvalidOperationException(Messages.NotFound));

            set.Remove(original);
            await _cocoricoDbContext.SaveChangesAsync();
            return new ServiceResult();
        }
    }
}
