using Cocorico.Server.Domain.Models;
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

        public async Task<SandwichResultDto> GetSandwichResultAsync(int id)
        {
            var sandwich = await Context
                               .Sandwiches
                               .SingleOrDefaultAsync(s => s.Id == id)
                           ?? throw new EntityNotFoundException($"Cant find sandwich with id:{id}");

            return sandwich.MapTo<Models.Entities.Sandwich, SandwichResultDto>();
        }

        public async Task<IEnumerable<SandwichResultDto>> GetAllSandwichResultAsync()
        {
            var sandwiches = await Context.Sandwiches.ToListAsync();

            var sandwichResultList = sandwiches.Select(s => s.MapTo<Models.Entities.Sandwich, SandwichResultDto>());

            return sandwichResultList;
        }

        public async Task AddOrUpdateSandwichAsync(NewSandwichDto newSandwichDto) =>
            await AddOrUpdateAsync(newSandwichDto.MapTo<NewSandwichDto, Models.Entities.Sandwich>());

        public async Task DeleteSandwichAsync(int id) =>
            await DeleteAsync(id);
    }
}
