﻿using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Authentication;
using Cocorico.Shared.Api.Users;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Users
{
    public sealed class GetUserClaimsHandler : RequestHandlerBase<GetUserClaims, ClaimsDto>
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public GetUserClaimsHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context,
            UserManager<CocoricoUser> userManager)
            : base(mediator, mapper, context) =>
            _userManager = userManager;

        public override async Task<ClaimsDto> Handle(GetUserClaims request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId) ?? throw new EntityNotFoundException($"User not found with id: {request.UserId}");

            var claims = await _userManager.GetClaimsAsync(user) ?? throw new UnexpectedException();

            return Mapper.Map<ClaimsDto>(claims);
        }
    }
}
