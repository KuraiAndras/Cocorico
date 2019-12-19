﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using MediatR;

namespace Cocorico.Application.Ingredients.Commands.AddIngredient
{
    public sealed class AddIngredientCommandHandler : CommandHandlerBase<AddIngredientCommand>
    {
        public AddIngredientCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(AddIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientToAdd = Mapper.Map<Ingredient>(request.Dto);

            await Context.Ingredients.AddAsync(ingredientToAdd, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}