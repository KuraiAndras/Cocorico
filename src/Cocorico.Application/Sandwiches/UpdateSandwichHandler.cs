﻿using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Orders;
using Cocorico.Shared.Api.Sandwiches;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches
{
    public sealed class UpdateSandwichHandler : HandlerBase<UpdateSandwich>
    {
        public UpdateSandwichHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(UpdateSandwich request, CancellationToken cancellationToken)
        {
            var canAddOrder = await Mediator.Send(new CanAddOrder { RequestTime = DateTime.Now }, cancellationToken);

            if (!canAddOrder) throw new StoreClosedException();

            var updatedSandwich = Mapper.Map<Sandwich>(request);

            var originalSandwich = await Context.Sandwiches
                .AsNoTracking()
                .Include(s => s.SandwichIngredients)
                .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (originalSandwich is null) throw new EntityNotFoundException();

            updatedSandwich.SandwichIngredients = originalSandwich.SandwichIngredients
                .Where(si => request.Ingredients.Any(i => si.SandwichId == i.Id))
                .ToList();

            var ingredientsInDb = await Context.Ingredients.ToListAsync(cancellationToken);

            foreach (var ingredientDto in request.Ingredients
                .Where(ingredientDto => updatedSandwich.SandwichIngredients.All(si => si.IngredientId != ingredientDto.Id)))
            {
                updatedSandwich.SandwichIngredients.Add(new SandwichIngredient
                {
                    Sandwich = updatedSandwich,
                    Ingredient = ingredientsInDb.Single(i => i.Id == ingredientDto.Id),
                });
            }

            Context.Sandwiches.Update(updatedSandwich);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
