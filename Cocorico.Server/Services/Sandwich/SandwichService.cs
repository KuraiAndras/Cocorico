using Cocorico.Server.Exceptions;
using Cocorico.Server.Extensions;
using Cocorico.Server.Helpers;
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

        public async Task<IServiceResult<SandwichResultDto>> GetAsync(int id)
        {
            var sandwich = await _cocoricoDbContext.Sandwiches.FirstOrDefaultAsync(s => s.Id == id);

            switch (sandwich)
            {
                case Models.Entities.Sandwich.Sandwich s: return new Success<SandwichResultDto>(s.ToSandwichResultDto());
                case null: return new Fail<SandwichResultDto>(new EntityNotFoundException());
            }
        }

        public async Task<IServiceResult<IEnumerable<SandwichResultDto>>> GetAllAsync()
        {
            var sandwiches = await _cocoricoDbContext
                .Sandwiches
                .Select(s => s.ToSandwichResultDto())
                .ToListAsync();

            switch (sandwiches)
            {
                case List<SandwichResultDto> s: return new Success<IEnumerable<SandwichResultDto>>(s);
                case null: return new Fail<IEnumerable<SandwichResultDto>>(new UnexpectedException());
            }
        }

        public async Task<IServiceResult> AddOrUpdateAsync(NewSandwichDto newSandwichDto)
        {
            var sandwich = newSandwichDto.ToSandwich();

            var set = _cocoricoDbContext.Sandwiches;

            var original = await set.SingleOrDefaultAsync(s => s.Id == sandwich.Id);

            if (!(original is null)) set.Remove(original);

            await set.AddAsync(sandwich);

            await _cocoricoDbContext.SaveChangesAsync();

            return new Success();
        }

        public async Task<IServiceResult> DeleteAsync(int id)
        {
            var set = _cocoricoDbContext.Sandwiches;

            var original = await set.SingleOrDefaultAsync(s => s.Id == id);

            if (original is null) return new Fail(new EntityNotFoundException());

            set.Remove(original);

            await _cocoricoDbContext.SaveChangesAsync();

            return new Success();
        }
    }
}
