using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using Cocorico.Shared.Dtos.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using Cocorico.Shared.Exceptions;

namespace Cocorico.Application.Users.Queries.GetClaims
{
    public sealed class GetUserClaimsQueryHandler : QueryHandlerBase<GetUserClaimsQuery, ClaimsDto>
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public GetUserClaimsQueryHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context,
            UserManager<CocoricoUser> userManager)
            : base(mediator, mapper, context) =>
            _userManager = userManager;

        public override async Task<ClaimsDto> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Dto.UserId) ?? throw new EntityNotFoundException($"User not found with id: {request.Dto.UserId}");

            // TODO: better exception
            var claims = await _userManager.GetClaimsAsync(user) ?? throw new UnexpectedException();

            return Mapper.Map<ClaimsDto>(claims);
        }
    }
}