using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users.Commands.RemoveClaimFromUser
{
    public sealed class RemoveClaimFromUserCommandHandler : CommandHandlerBase<RemoveClaimFromUserCommand>
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public RemoveClaimFromUserCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context,
            UserManager<CocoricoUser> userManager)
            : base(mediator, mapper, context) => _userManager = userManager;

        protected override async Task Handle(RemoveClaimFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Dto.UserId)
                       ?? throw new EntityNotFoundException($"User not found with id:{request.Dto.UserId}");

            // Sign user out
            await _userManager.UpdateSecurityStampAsync(user);

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claimToRemove = new Claim(ClaimTypes.Role, request.Dto.CocoricoClaim.ClaimValue, ClaimValueTypes.String);
            var oldClaim = userClaims.SingleOrDefault(c => c.Value.Equals(claimToRemove.Value, StringComparison.InvariantCulture));

            if (oldClaim is null) throw new InvalidCommandException();

            var removeClaimResult = await _userManager.RemoveClaimAsync(user, claimToRemove);
            if (!removeClaimResult.Succeeded) throw new UnexpectedException();

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
