using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Users;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users
{
    public sealed class GetAllUsersForAdminHandler : RequestHandlerBase<GetAllUsersForAdmin, ICollection<UserForAdminPage>>
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public GetAllUsersForAdminHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context,
            UserManager<CocoricoUser> userManager)
            : base(mediator, mapper, context) =>
            _userManager = userManager;

        public override async Task<ICollection<UserForAdminPage>> Handle(GetAllUsersForAdmin request, CancellationToken cancellationToken)
        {
            var users = await Context.Users.ToListAsync(cancellationToken) ?? throw new UnexpectedException();

            var usersForAdminPage = new List<UserForAdminPage>();
            foreach (var cocoricoUser in users)
            {
                var claims = (await _userManager.GetClaimsAsync(cocoricoUser)).Select(c => c.Value).ToList();

                var userForAdminPage = Mapper.Map<UserForAdminPage>(cocoricoUser);
                userForAdminPage.Claims = claims;

                usersForAdminPage.Add(userForAdminPage);
            }

            return usersForAdminPage;
        }
    }
}
