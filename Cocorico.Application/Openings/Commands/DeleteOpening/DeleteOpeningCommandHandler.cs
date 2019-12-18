﻿using AutoMapper;
using Cocorico.Application.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Openings.Commands.DeleteOpening
{
    public sealed class DeleteOpeningCommandHandler : CommandHandlerBase<DeleteOpeningCommand>
    {
        public DeleteOpeningCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(DeleteOpeningCommand request, CancellationToken cancellationToken)
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