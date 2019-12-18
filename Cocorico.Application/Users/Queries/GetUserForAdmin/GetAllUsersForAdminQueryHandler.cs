using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users.Queries.GetUserForAdmin
{
    public sealed class GetAllUsersForAdminQueryHandler : QueryHandlerBase<GetAllUsersForAdminQuery, ICollection<UserForAdminPage>>
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public GetAllUsersForAdminQueryHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context,
            UserManager<CocoricoUser> userManager)
            : base(mediator, mapper, context) =>
            _userManager = userManager;

        public override async Task<ICollection<UserForAdminPage>> Handle(GetAllUsersForAdminQuery request, CancellationToken cancellationToken)
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