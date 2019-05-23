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

        public async Task<SandwichDto> GetSandwichResultAsync(int id)
        {
            var sandwich = await Context
                               .Sandwiches
                               .Include(s => s.Ingredients)
                               .SingleOrDefaultAsync(s => s.Id == id)
                           ?? throw new EntityNotFoundException($"Cant find sandwich with id:{id}");

            return sandwich.MapTo<Models.Entities.Sandwich, SandwichDto>();
        }

        public async Task<IEnumerable<SandwichDto>> GetAllSandwichResultAsync()
        {
            var sandwiches = await Context
                .Sandwiches
                .Include(s => s.Ingredients)
                .ToListAsync();

            var sandwichResultList = sandwiches.Select(s => s.MapTo<Models.Entities.Sandwich, SandwichDto>());

            return sandwichResultList;
        }

        public async Task AddSandwichAsync(SandwichAddDto newSandwichDto)
        {
            var ingredients = await Context
                .Ingredients
                .ToListAsync();

            await AddAsync(new Models.Entities.Sandwich
            {
                Id = 0,
                IsDeleted = false,
                Name = newSandwichDto.Name,
                Price = newSandwichDto.Price,
                Ingredients = ingredients.Where(i => newSandwichDto.Ingredients.SingleOrDefault(iDto => iDto.Id == i.Id) != null).ToList()
            });
        }

        public async Task UpdateSandwichAsync(SandwichDto sandwichDto) =>
            await UpdateAsync(new Models.Entities.Sandwich
            {
                Id = sandwichDto.Id,
                IsDeleted = false,
                Name = sandwichDto.Name,
                Price = sandwichDto.Price,
            });

        public async Task DeleteSandwichAsync(int id) =>
            await DeleteByIdAsync(id);
    }
}
