﻿using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Ingredients.Commands.DeleteIngredient
{
    public sealed class DeleteIngredientCommandHandler : CommandHandlerBase<DeleteIngredientCommand>
    {
        public DeleteIngredientCommandHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientToRemove = await Context.Ingredients
                .SingleOrDefaultAsync(i => i.Id.Equals(request.Dto), cancellationToken);

            if (ingredientToRemove is null) throw new EntityNotFoundException();

            Context.Ingredients.Remove(ingredientToRemove);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}