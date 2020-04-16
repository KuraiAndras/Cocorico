using AutoMapper;
using Cocorico.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application
{
    public abstract class CommandHandlerBase<T> : AsyncRequestHandler<T>
        where T : IRequest
    {
        protected CommandHandlerBase(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
        {
            Mediator = mediator;
            Mapper = mapper;
            Context = context;
        }

        protected CocoricoDbContext Context { get; }
        protected IMapper Mapper { get; }

        protected IMediator Mediator { get; }

        protected abstract override Task Handle(T request, CancellationToken cancellationToken);
    }
}
