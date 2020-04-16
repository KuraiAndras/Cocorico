using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches.Commands.AddSandwich
{
    public sealed class AddSandwichCommandHandler : CommandHandlerBase<AddSandwichCommand>
    {
        public AddSandwichCommandHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(AddSandwichCommand request, CancellationToken cancellationToken)
        {
            var sandwich = Mapper.Map<Sandwich>(request.Dto);

            var ingredients = await Context
                .Ingredients
                .ToListAsync(cancellationToken);

            sandwich.SandwichIngredients = ingredients
                .Where(i => request.Dto.Ingredients.Any(iDto => iDto.Id == i.Id))
                .Select(i => new SandwichIngredient
                {
                    Ingredient = i,
                    Sandwich = sandwich,
                })
                .ToList();

            Context.Sandwiches.Add(sandwich);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
