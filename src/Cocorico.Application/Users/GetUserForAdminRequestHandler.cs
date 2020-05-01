using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Users;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users
{
    public sealed class GetUserForAdminRequestHandler : RequestHandlerBase<GetUserForAdminQuery, UserForAdminPage>
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public GetUserForAdminRequestHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context,
            UserManager<CocoricoUser> userManager)
            : base(mediator, mapper, context) => _userManager = userManager;

        public override async Task<UserForAdminPage> Handle(GetUserForAdminQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId) ?? throw new EntityNotFoundException($"Cant find user with id:{request.UserId}");

            var userClaims = await _userManager.GetClaimsAsync(user);

            var userForAdminPage = Mapper.Map<UserForAdminPage>(user);
            userForAdminPage.Claims = userClaims.Select(c => c.Value).ToList();

            return userForAdminPage;
        }
    }
}
