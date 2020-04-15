using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users.Queries.GetClaims
{
    public sealed class GetUserClaimsByNameQueryHandler : QueryHandlerBase<GetUserClaimsByNameQuery, ClaimsDto>
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public GetUserClaimsByNameQueryHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context,
            UserManager<CocoricoUser> userManager)
            : base(mediator, mapper, context) =>
            _userManager = userManager;

        public override async Task<ClaimsDto> Handle(GetUserClaimsByNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Dto.UserName) ?? throw new EntityNotFoundException($"User not found with name: {request.Dto.UserName}");

            var claims = await _userManager.GetClaimsAsync(user) ?? throw new UnexpectedException();

            return Mapper.Map<ClaimsDto>(claims);
        }
    }
}
