using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Api.Sandwiches;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches
{
    public class DeleteSandwichHandler : HandlerBase<DeleteSandwich>
    {
        public DeleteSandwichHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(DeleteSandwich request, CancellationToken cancellationToken)
        {
            var sandwichToDelete = await Context.Sandwiches.SingleOrDefaultAsync(s => s.Id.Equals(request.SandwichId), cancellationToken);

            Context.Sandwiches.Remove(sandwichToDelete);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
