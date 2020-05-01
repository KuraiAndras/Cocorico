using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Api.Ingredients;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Ingredients
{
    public sealed class DeleteIngredientHandler : HandlerBase<DeleteIngredient>
    {
        public DeleteIngredientHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(DeleteIngredient request, CancellationToken cancellationToken)
        {
            var ingredientToRemove = await Context.Ingredients
                .SingleOrDefaultAsync(i => i.Id.Equals(request.Id), cancellationToken);

            if (ingredientToRemove is null) throw new EntityNotFoundException();

            Context.Ingredients.Remove(ingredientToRemove);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
