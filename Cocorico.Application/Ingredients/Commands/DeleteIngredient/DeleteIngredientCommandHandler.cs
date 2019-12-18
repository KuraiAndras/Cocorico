using AutoMapper;
using Cocorico.Application.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Cocorico.Shared.Exceptions;

namespace Cocorico.Application.Ingredients.Commands.DeleteIngredient
{
    public sealed class DeleteIngredientCommandHandler : CommandHandlerBase<DeleteIngredientCommand>
    {
        public DeleteIngredientCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientToRemove = await Context.Ingredients
                .SingleOrDefaultAsync(i => i.Id.Equals(request.Id), cancellationToken);

            if (ingredientToRemove is null) throw new EntityNotFoundException();

            Context.Ingredients.Remove(ingredientToRemove);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}