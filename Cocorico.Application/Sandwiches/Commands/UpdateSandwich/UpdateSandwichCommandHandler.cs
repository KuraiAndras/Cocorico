using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Application.Orders.Queries.CanAddOrder;
using Cocorico.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cocorico.Shared.Exceptions;

namespace Cocorico.Application.Sandwiches.Commands.UpdateSandwich
{
    public sealed class UpdateSandwichCommandHandler : CommandHandlerBase<UpdateSandwichCommand>
    {
        public UpdateSandwichCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(UpdateSandwichCommand request, CancellationToken cancellationToken)
        {
            var dateAdded = DateTime.Now;

            var canAddOrder = await Mediator.Send(new CanAddOrderQuery(dateAdded), cancellationToken);

            if (!canAddOrder) throw new StoreClosedException();

            var updatedSandwich = Mapper.Map<Sandwich>(request.Dto);

            var originalSandwich = await Context.Sandwiches
                .AsNoTracking()
                .Include(s => s.SandwichIngredients)
                .SingleOrDefaultAsync(s => s.Id == request.Dto.Id, cancellationToken);

            if (originalSandwich is null) throw new EntityNotFoundException();

            updatedSandwich.SandwichIngredients = originalSandwich.SandwichIngredients
                .Where(si => request.Dto.Ingredients.Any(i => si.SandwichId == i.Id))
                .ToList();

            var ingredientsInDb = await Context.Ingredients.ToListAsync(cancellationToken);

            foreach (var ingredientDto in request.Dto.Ingredients
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
