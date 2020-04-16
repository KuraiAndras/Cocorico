using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users.Commands.LoginUser
{
    public sealed class LoginUserHandler : HandlerBase<LoginUserCommand>
    {
        private readonly UserManager<CocoricoUser> _userManager;
        private readonly SignInManager<CocoricoUser> _signInManager;

        public LoginUserHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context,
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
