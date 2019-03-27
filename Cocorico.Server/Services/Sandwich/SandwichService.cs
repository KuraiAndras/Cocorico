using Cocorico.Server.Extensions;
using Cocorico.Server.Models;
using Cocorico.Shared.Dtos.Sandwich;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Services.Sandwich
{
    public class SandwichService : ISandwichService
    {
        private readonly CocoricoDbContext _cocoricoDbContext;

        public SandwichService(CocoricoDbContext cocoricoDbContext) => _cocoricoDbContext = cocoricoDbContext;

        public async Task<SandwichResultDto> GetAsync(int id)
        {
            var sandwich = await _cocoricoDbContext.Sandwiches.FirstOrDefaultAsync(s => s.Id == id);

            return sandwich.ToSandwichResultDto();
        }

        public async Task<IEnumerable<SandwichResultDto>> GetAllAsync()
        {
            var sandwiches = await _cocoricoDbContext.Sandwiches.ToListAsync();

            return sandwiches.Select(s => s.ToSandwichResultDto());
        }

        public async Task AddOrUpdateAsync(NewSandwichDto newSandwichDto)
        {
            var sandwich = newSandwichDto.ToSandwich();

            var set = _cocoricoDbContext.Sandwiches;

            var original = await set.SingleOrDefaultAsync(s => s.Id == sandwich.Id);

            if (!(original is null)) set.Remove(original);

            await set.AddAsync(sandwich);

            await _cocoricoDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var set = _cocoricoDbContext.Sandwiches;

            var original = await set.SingleOrDefaultAsync(s => s.Id == id);

            if (!(original is null)) set.Remove(original);

            await _cocoricoDbContext.SaveChangesAsync();
        }
    }
}
