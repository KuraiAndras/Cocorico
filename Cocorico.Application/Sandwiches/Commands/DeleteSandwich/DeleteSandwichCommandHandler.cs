using AutoMapper;
using Cocorico.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches.Commands.DeleteSandwich
{
    public class DeleteSandwichCommandHandler : CommandHandlerBase<DeleteSandwichCommand>
    {
        public DeleteSandwichCommandHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(DeleteSandwichCommand request, CancellationToken cancellationToken)
        {
            var sandwichToDelete = await Context.Sandwiches.SingleOrDefaultAsync(s => s.Id.Equals(request.Dto), cancellationToken);

            Context.Sandwiches.Remove(sandwichToDelete);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
