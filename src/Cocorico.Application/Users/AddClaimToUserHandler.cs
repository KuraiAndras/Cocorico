using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Users;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users
{
    public sealed class AddClaimToUserHandler : HandlerBase<AddClaimToUser>
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public AddClaimToUserHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context,
            UserManager<CocoricoUser> userManager)
            : base(mediator, mapper, context) =>
            _userManager = userManager;

        protected override async Task Handle(AddClaimToUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId)
                       ?? throw new EntityNotFoundException($"User not found with id: {request.UserId}");

            // Sign user out
            await _userManager.UpdateSecurityStampAsync(user);

            var userClaims = await _userManager.GetClaimsAsync(user);
            var newClaim = new Claim(ClaimTypes.Role, request.CocoricoClaim.ClaimValue, ClaimValueTypes.String);
            var oldClaim = userClaims.SingleOrDefault(c => c.Value.Equals(newClaim.Value, StringComparison.InvariantCulture));

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
