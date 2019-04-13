using Cocorico.Server.Models;
using Cocorico.Server.Services.ServiceBase;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Services.Sandwich
{
    public class ServerSandwichService : EntityServiceBase<Models.Entities.Sandwich.Sandwich>, IServerSandwichService
    {
        public ServerSandwichService(CocoricoDbContext context) : base(context)
        {
        }

        public async Task<IServiceResult<SandwichResultDto>> GetSandwichResult(int id)
        {
            var sandwich = await Context.Sandwiches.FirstOrDefaultAsync(s => s.Id == id);

            if (sandwich is null) return new Fail<SandwichResultDto>(new EntityNotFoundException());

            return new Success<SandwichResultDto>(sandwich.MapTo<Models.Entities.Sandwich.Sandwich, SandwichResultDto>());
        }

        public async Task<IServiceResult<IEnumerable<SandwichResultDto>>> GetAllSandwichResultAsync()
        {
            var sandwiches = Context.Sandwiches;

            var sandwichResultList = await sandwiches.Select(s => s.MapTo<Models.Entities.Sandwich.Sandwich, SandwichResultDto>()).ToListAsync();

            switch (sandwichResultList)
            {
                case IEnumerable<SandwichResultDto> enumerable: return new Success<IEnumerable<SandwichResultDto>>(enumerable);
                default: return new Fail<IEnumerable<SandwichResultDto>>(new UnexpectedException());
            }
        }

        public async Task<IServiceResult> AddOrUpdateSandwichAsync(NewSandwichDto newSandwichDto) =>
            await AddOrUpdateAsync(newSandwichDto.MapTo<NewSandwichDto, Models.Entities.Sandwich.Sandwich>());

        public async Task<IServiceResult> DeleteSandwichAsync(int id) =>
            await DeleteAsync(id);
    }
}
