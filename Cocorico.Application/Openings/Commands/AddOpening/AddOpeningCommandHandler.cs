﻿using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Openings.Commands.AddOpening
{
    public sealed class AddOpeningCommandHandler : CommandHandlerBase<AddOpeningCommand>
    {
        public AddOpeningCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(AddOpeningCommand request, CancellationToken cancellationToken)
        {
            if (request.Dto.Start is null) throw new ArgumentException(nameof(request.Dto));
            if (request.Dto.End is null) throw new ArgumentException(nameof(request.Dto));
            if (request.Dto.Start.Value > request.Dto.End.Value) throw new InvalidOperationException("Start is sooner than End");

            var opening = Mapper.Map<Opening>(request.Dto);

            var openingsInDb = await Context.Openings.AsNoTracking().ToListAsync(cancellationToken);

            var lastOpening = openingsInDb.OrderByDescending(o => o.End).FirstOrDefault()
                              ?? new Opening { End = new DateTime(), Start = new DateTime() };

            if (lastOpening.End > opening.Start) throw new InvalidOperationException("New start is sooner than last end");

            Context.Openings.Add(opening);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
