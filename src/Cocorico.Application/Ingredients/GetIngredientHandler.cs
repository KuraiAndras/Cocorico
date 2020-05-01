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
    public sealed class GetIngredientHandler : RequestHandlerBase<GetIngredient, IngredientDto>
    {
        public GetIngredientHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<IngredientDto> Handle(GetIngredient request, CancellationToken cancellationToken)
        {
            var result = await Context.Ingredients.SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken)
                         ?? throw new EntityNotFoundException();

            return Mapper.Map<IngredientDto>(result);
        }
    }
}
