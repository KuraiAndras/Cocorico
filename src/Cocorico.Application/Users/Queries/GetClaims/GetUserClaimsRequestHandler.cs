using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users.Queries.GetClaims
{
    public sealed class GetUserClaimsRequestHandler : RequestHandlerBase<GetUserClaimsQuery, ClaimsDto>
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public GetUserClaimsRequestHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context,
            UserManager<CocoricoUser> userManager)
            : base(mediator, mapper, context) =>
            _userManager = userManager;

        public override async Task<ClaimsDto> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Dto.UserId) ?? throw new EntityNotFoundException($"User not found with id: {request.Dto.UserId}");

            var claims = await _userManager.GetClaimsAsync(user) ?? throw new UnexpectedException();

            return Mapper.Map<ClaimsDto>(claims);
        }
    }
}
