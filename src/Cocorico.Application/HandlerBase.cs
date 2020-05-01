using AutoMapper;
using Cocorico.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application
{
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class HandlerBase<T> : AsyncRequestHandler<T>
        where T : IRequest
    {
        protected HandlerBase(
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

    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected RequestHandlerBase(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
        {
            Mediator = mediator;
            Mapper = mapper;
            Context = context;
        }

        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }
        protected CocoricoDbContext Context { get; }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
#pragma warning restore SA1402 // File may only contain a single type
}
