using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Api.Openings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Openings
{
    public sealed class DeleteOpeningHandler : HandlerBase<DeleteOpening>
    {
        public DeleteOpeningHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(DeleteOpening request, CancellationToken cancellationToken)
        {
            var opening = await Context.Openings
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (opening is null) return;

            Context.Openings.Remove(opening);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
