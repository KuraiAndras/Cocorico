using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Openings;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Openings
{
    public sealed class UpdateOpeningHandler : HandlerBase<UpdateOpening>
    {
        public UpdateOpeningHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(UpdateOpening request, CancellationToken cancellationToken)
        {
            if (request.Start is null) throw new ArgumentException(nameof(request));
            if (request.End is null) throw new ArgumentException(nameof(request));
            if (request.Start.Value > request.End.Value) throw new ArgumentException(nameof(request));

            var opening = Mapper.Map<Opening>(request);

            Context.Openings.Update(opening);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
