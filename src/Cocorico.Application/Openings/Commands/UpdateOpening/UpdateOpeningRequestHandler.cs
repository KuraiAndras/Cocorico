using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Openings.Commands.UpdateOpening
{
    public sealed class UpdateOpeningRequestHandler : RequestHandlerBase<UpdateOpeningCommand>
    {
        public UpdateOpeningRequestHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(UpdateOpeningCommand request, CancellationToken cancellationToken)
        {
            if (request.Dto.Start is null) throw new ArgumentException(nameof(request.Dto));
            if (request.Dto.End is null) throw new ArgumentException(nameof(request.Dto));
            if (request.Dto.Start.Value > request.Dto.End.Value) throw new ArgumentException(nameof(request.Dto));

            var opening = Mapper.Map<Opening>(request.Dto);

            Context.Openings.Update(opening);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
