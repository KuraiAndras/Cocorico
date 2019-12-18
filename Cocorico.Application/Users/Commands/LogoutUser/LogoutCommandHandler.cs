using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cocorico.Application.Users.Commands.LogoutUser
{
    public sealed class LogoutCommandHandler : CommandHandlerBase<LogoutCurrentUserCommand>
    {
        private readonly SignInManager<CocoricoUser> _signInManager;

        public LogoutCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context,
            SignInManager<CocoricoUser> signInManager)
            : base(mediator, mapper, context) =>
            _signInManager = signInManager;

        protected override async Task Handle(LogoutCurrentUserCommand request, CancellationToken cancellationToken) => await _signInManager.SignOutAsync();
    }
}