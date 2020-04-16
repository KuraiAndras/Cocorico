using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users.Commands.LogoutUser
{
    public sealed class LogoutCurrentUserCommandHandler : CommandHandlerBase<LogoutCurrentUserCommand>
    {
        private readonly SignInManager<CocoricoUser> _signInManager;

        public LogoutCurrentUserCommandHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context,
            SignInManager<CocoricoUser> signInManager)
            : base(mediator, mapper, context) =>
            _signInManager = signInManager;

        protected override async Task Handle(LogoutCurrentUserCommand request, CancellationToken cancellationToken) => await _signInManager.SignOutAsync();
    }
}
