﻿using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Exceptions;
using Cocorico.Shared.Dtos.Ingredient;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Ingredients.Queries.GetIngredient
{
    public sealed class GetIngredientQueryHandler : QueryHandlerBase<GetIngredientQuery, IngredientDto>
    {
        public GetIngredientQueryHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<IngredientDto> Handle(GetIngredientQuery request, CancellationToken cancellationToken)
        {
            var result = await Context.Ingredients.SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken)
                         ?? throw new EntityNotFoundException();

            return Mapper.Map<IngredientDto>(result);
        }
    }
}