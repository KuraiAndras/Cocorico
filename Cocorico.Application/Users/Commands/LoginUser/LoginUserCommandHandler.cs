using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using Cocorico.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cocorico.Application.Users.Commands.LoginUser
{
    public sealed class LoginUserCommandHandler : CommandHandlerBase<LoginUserCommand>
    {
        private readonly UserManager<CocoricoUser> _userManager;
        private readonly SignInManager<CocoricoUser> _signInManager;

        public LoginUserCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context,
            UserManager<CocoricoUser> userManager,
            SignInManager<CocoricoUser> signInManager)
            : base(mediator, mapper, context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected override async Task Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Dto.Email) ?? throw new EntityNotFoundException($"User not found with email: {request.Dto.Email}");

            var login = await _signInManager.PasswordSignInAsync(request.Dto.Email, request.Dto.Password, true, false);
            if (login != SignInResult.Success) throw new InvalidCredentialsException();

            await _userManager.UpdateAsync(user);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}