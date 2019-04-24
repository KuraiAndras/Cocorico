﻿using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Services.Helpers;
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

        public async Task<IServiceResult<SandwichResultDto>> GetSandwichResultAsync(int id)
        {
            var sandwich = await Context.Sandwiches.FirstOrDefaultAsync(s => s.Id == id);

            if (sandwich is null) return new Fail<SandwichResultDto>(new EntityNotFoundException());

            return new Success<SandwichResultDto>(sandwich.MapTo<Models.Entities.Sandwich, SandwichResultDto>());
        }

        public async Task<IServiceResult<IEnumerable<SandwichResultDto>>> GetAllSandwichResultAsync()
        {
            var sandwiches = await Context.Sandwiches.ToListAsync();

            var sandwichResultList = sandwiches.Select(s => s.MapTo<Models.Entities.Sandwich, SandwichResultDto>());

            switch (sandwichResultList)
            {
                case IEnumerable<SandwichResultDto> enumerable: return new Success<IEnumerable<SandwichResultDto>>(enumerable);
                default: return new Fail<IEnumerable<SandwichResultDto>>(new UnexpectedException());
            }
        }

        public async Task<IServiceResult> AddOrUpdateSandwichAsync(NewSandwichDto newSandwichDto) =>
            await AddOrUpdateAsync(newSandwichDto.MapTo<NewSandwichDto, Models.Entities.Sandwich>());

        public async Task<IServiceResult> DeleteSandwichAsync(int id) =>
            await DeleteAsync(id);
    }
}
