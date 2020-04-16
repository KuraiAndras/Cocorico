using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Openings
{
    public sealed class AddOpeningHandler : HandlerBase<Shared.Api.Openings.AddOpening>
    {
        public AddOpeningHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(Shared.Api.Openings.AddOpening request, CancellationToken cancellationToken)
        {
            if (request.Start is null) throw new ArgumentException(nameof(request));
            if (request.End is null) throw new ArgumentException(nameof(request));
            if (request.Start.Value > request.End.Value) throw new InvalidOperationException("Start is sooner than End");

            var opening = Mapper.Map<Opening>(request);

            var openingsInDb = await Context.Openings.AsNoTracking().ToListAsync(cancellationToken);

            var lastOpening = openingsInDb.OrderByDescending(o => o.End).FirstOrDefault()
                              ?? new Opening { End = new DateTime(), Start = new DateTime() };

            if (lastOpening.End > opening.Start) throw new InvalidOperationException("New start is sooner than last end");

            Context.Openings.Add(opening);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
