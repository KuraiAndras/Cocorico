using AutoMapper;
using Cocorico.Application.Common.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application
{
    public abstract class CommandHandlerBase<T> : AsyncRequestHandler<T> where T : IRequest
    {
        protected CommandHandlerBase(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
        {
            Mediator = mediator;
            Mapper = mapper;
            Context = context;
        }

        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }
        protected ICocoricoDbContext Context { get; }

        protected abstract override Task Handle(T request, CancellationToken cancellationToken);
    }
}
