using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Sandwiches;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches
{
    public sealed class AddSandwichHandler : HandlerBase<AddSandwich>
    {
        public AddSandwichHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(AddSandwich request, CancellationToken cancellationToken)
        {
            var sandwich = Mapper.Map<Sandwich>(request);

            var ingredients = await Context
                .Ingredients
                .ToListAsync(cancellationToken);

            sandwich.SandwichIngredients = ingredients
                .Where(i => request.Ingredients.Any(iDto => iDto.Id == i.Id))
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
