using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using Cocorico.Domain.Exceptions;
using Cocorico.Shared.Dtos.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cocorico.Application.Users.Queries.GetClaims
{
    public sealed class GetClaimsQueryHandler : QueryHandlerBase<GetClaimsQuery, ClaimsDto>
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public GetClaimsQueryHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context,
            UserManager<CocoricoUser> userManager)
            : base(mediator, mapper, context) =>
            _userManager = userManager;

        public override async Task<ClaimsDto> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Dto.Email) ?? throw new EntityNotFoundException($"User not found with email: {request.Dto.Email}");

            // TODO: better exception
            var claims = await _userManager.GetClaimsAsync(user) ?? throw new UnexpectedException();

            return new ClaimsDto { Claims = claims.ToList() };
        }
    }
}