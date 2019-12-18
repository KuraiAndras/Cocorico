﻿using AutoMapper;
using Cocorico.Application.Common.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application
{
    public abstract class QueryHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected QueryHandlerBase(
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

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}