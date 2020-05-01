using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users
{
    public sealed class LogoutCurrentUserHandler : HandlerBase<LogoutCurrentUser>
    {
        private readonly SignInManager<CocoricoUser> _signInManager;

        public LogoutCurrentUserHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context,
            SignInManager<CocoricoUser> signInManager)
            : base(mediator, mapper, context) =>
            _signInManager = signInManager;

        protected override async Task Handle(LogoutCurrentUser request, CancellationToken cancellationToken) => await _signInManager.SignOutAsync();
    }
}
