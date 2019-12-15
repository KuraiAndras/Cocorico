using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Application.Orders.Queries.CanAddOrder;
using Cocorico.Domain.Entities;
using Cocorico.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches.Commands.UpdateSandwich
{
    public sealed class UpdateSandwichCommandHandler : AsyncRequestHandler<UpdateSandwichCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICocoricoDbContext _context;
        private readonly IMediator _mediator;

        public UpdateSandwichCommandHandler(
            IMapper mapper,
            ICocoricoDbContext context,
            IMediator mediator)
        {
            _mapper = mapper;
            _context = context;
            _mediator = mediator;
        }

        protected override async Task Handle(UpdateSandwichCommand request, CancellationToken cancellationToken)
        {
            var dateAdded = DateTime.Now;

            var canAddOrder = await _mediator.Send(new CanAddOrderQuery(dateAdded), cancellationToken);

            if (!canAddOrder) throw new StoreClosedException();

            var updatedSandwich = _mapper.Map<Sandwich>(request.Dto);

            var originalSandwich = await _context.Sandwiches
                .AsNoTracking()
                .Include(s => s.SandwichIngredients)
                .SingleOrDefaultAsync(s => s.Id == request.Dto.Id, cancellationToken);

            if (originalSandwich is null) throw new EntityNotFoundException();

            updatedSandwich.SandwichIngredients = originalSandwich.SandwichIngredients
                .Where(si => request.Dto.Ingredients.Any(i => si.SandwichId == i.Id))
                .ToList();

            var ingredientsInDb = await _context.Ingredients.ToListAsync(cancellationToken);

            foreach (var ingredientDto in request.Dto.Ingredients
                .Where(ingredientDto => updatedSandwich.SandwichIngredients.All(si => si.IngredientId != ingredientDto.Id)))
            {
                updatedSandwich.SandwichIngredients.Add(new SandwichIngredient
                {
                    Sandwich = updatedSandwich,
                    Ingredient = ingredientsInDb.Single(i => i.Id == ingredientDto.Id),
                });
            }

            _context.Sandwiches.Update(updatedSandwich);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
