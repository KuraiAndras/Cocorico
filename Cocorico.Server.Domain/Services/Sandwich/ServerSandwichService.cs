using System;
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

        //TODO: Create new and update sandwich dtos
        public async Task AddSandwichAsync(NewSandwichDto newSandwichDto) =>
            await AddAsync(new Models.Entities.Sandwich
            {
                Id = 0,
                IsDeleted = false,
                Name = newSandwichDto.Name,
                Price = newSandwichDto.Price,
            });

        public async Task UpdateSandwichAsync(NewSandwichDto newSandwichDto) =>
            await UpdateAsync(new Models.Entities.Sandwich
            {
                Id = newSandwichDto.Id,
                IsDeleted = false,
                Name = newSandwichDto.Name,
                Price = newSandwichDto.Price,
            });

        public async Task DeleteSandwichAsync(int id) =>
            await DeleteByIdAsync(id);
    }
}
