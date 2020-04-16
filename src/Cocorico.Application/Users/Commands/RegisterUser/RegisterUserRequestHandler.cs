using AutoMapper;
using Cocorico.Application.Users.Services;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users.Commands.RegisterUser
{
    public class RegisterUserRequestHandler : RequestHandlerBase<RegisterUserCommand>
    {
        private readonly UserManager<CocoricoUser> _userManager;
        private readonly IClaimService _claimService;

        public RegisterUserRequestHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context,
            UserManager<CocoricoUser> userManager,
            IClaimService claimService)
            : base(mediator, mapper, context)
        {
            _userManager = userManager;
            _claimService = claimService;
        }

        protected override async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userIdentity = Mapper.Map<CocoricoUser>(request.Dto);

            // TODO: Fix this
            userIdentity.UserName = userIdentity.Email;

            var result = await _userManager.CreateAsync(userIdentity, request.Dto.Password);

            // TODO: Better exception
            if (!result.Succeeded) throw new UnexpectedException();

            var customerClaim = await _claimService.GetBasicUserClaimsAsync();

            var claimResult = await _userManager.AddClaimsAsync(userIdentity, customerClaim);

            if (!claimResult.Succeeded) throw new UnexpectedException();

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
