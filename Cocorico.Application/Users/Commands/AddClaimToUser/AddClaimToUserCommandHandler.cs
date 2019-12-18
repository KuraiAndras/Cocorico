using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users.Commands.AddClaimToUser
{
    public sealed class AddClaimToUserCommandHandler : CommandHandlerBase<AddClaimToUserCommand>
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public AddClaimToUserCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context,
            UserManager<CocoricoUser> userManager)
            : base(mediator, mapper, context) =>
            _userManager = userManager;

        protected override async Task Handle(AddClaimToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Dto.UserId)
                       ?? throw new EntityNotFoundException($"User not found with id: {request.Dto.UserId}");

            //Sign user out
            await _userManager.UpdateSecurityStampAsync(user);

            var userClaims = await _userManager.GetClaimsAsync(user);
            var newClaim = new Claim(ClaimTypes.Role, request.Dto.CocoricoClaim.ClaimValue, ClaimValueTypes.String);
            var oldClaim = userClaims.SingleOrDefault(c => c.Value.Equals(newClaim.Value));

            if (!(oldClaim is null))
            {
                var removeClaimResult = await _userManager.RemoveClaimAsync(user, oldClaim);
                if (!removeClaimResult.Succeeded) throw new UnexpectedException();
            }

            var addClaimResult = await _userManager.AddClaimAsync(user, newClaim);
            if (!addClaimResult.Succeeded) throw new UnexpectedException();

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}