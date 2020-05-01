using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Api.Sandwiches;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches
{
    public class GetSandwichHandler : RequestHandlerBase<GetSandwich, SandwichDto>
    {
        public GetSandwichHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<SandwichDto> Handle(GetSandwich request, CancellationToken cancellationToken)
        {
            var sandwich = await Context
                               .Sandwiches
                               .Include(s => s.SandwichIngredients)
                               .ThenInclude(il => il.Ingredient)
                               .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
                           ?? throw new EntityNotFoundException($"Cant find sandwich with id:{request.Id}");

            return Mapper.Map<SandwichDto>(sandwich);
        }
    }
}
