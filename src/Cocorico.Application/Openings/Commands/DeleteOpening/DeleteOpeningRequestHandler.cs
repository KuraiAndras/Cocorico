using AutoMapper;
using Cocorico.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Openings.Commands.DeleteOpening
{
    public sealed class DeleteOpeningRequestHandler : RequestHandlerBase<DeleteOpeningCommand>
    {
        public DeleteOpeningRequestHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(DeleteOpeningCommand request, CancellationToken cancellationToken)
        {
            var opening = await Context.Openings
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == request.Dto, cancellationToken);

            if (opening is null) return;

            Context.Openings.Remove(opening);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
