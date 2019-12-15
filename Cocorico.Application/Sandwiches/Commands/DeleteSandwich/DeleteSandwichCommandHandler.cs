using AutoMapper;
using Cocorico.Application.Common.Persistence;
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
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(DeleteSandwichCommand request, CancellationToken cancellationToken)
        {
            var sandwichToDelete = await Context.Sandwiches.SingleOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);

            Context.Sandwiches.Remove(sandwichToDelete);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
