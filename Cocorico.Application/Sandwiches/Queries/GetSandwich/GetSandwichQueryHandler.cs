﻿using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Exceptions;
using Cocorico.Shared.Dtos.Sandwich;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches.Queries.GetSandwich
{
    public class GetSandwichQueryHandler : QueryHandlerBase<GetSandwichQuery, SandwichDto>
    {
        public GetSandwichQueryHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<SandwichDto> Handle(GetSandwichQuery request, CancellationToken cancellationToken)
        {
            var sandwich = await Context
                               .Sandwiches
                               .Include(s => s.SandwichIngredients)
                               .ThenInclude(il => il.Ingredient)
                               .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
                           ?? throw new EntityNotFoundException($"Cant find sandwich with id:{request.Id}");

            return Mapper.Map<SandwichDto>(sandwich);
        }
    }
}