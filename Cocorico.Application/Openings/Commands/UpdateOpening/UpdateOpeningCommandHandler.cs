using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using MediatR;

namespace Cocorico.Application.Openings.Commands.UpdateOpening
{
    public sealed class UpdateOpeningCommandHandler : CommandHandlerBase<UpdateOpeningCommand>
    {
        public UpdateOpeningCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(UpdateOpeningCommand request, CancellationToken cancellationToken)
        {
            // TODO: fluent validator
            if (!request.Dto.Start.HasValue) throw new ArgumentException(nameof(request.Dto));
            if (!request.Dto.End.HasValue) throw new ArgumentException(nameof(request.Dto));
            if (request.Dto.Start.Value > request.Dto.End.Value) throw new ArgumentException(nameof(request.Dto));

            var opening = Mapper.Map<Opening>(request.Dto);

            Context.Openings.Update(opening);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}