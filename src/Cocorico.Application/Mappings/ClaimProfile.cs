using AutoMapper;
using Cocorico.Shared.Api.Authentication;
using System.Collections.Generic;
using System.Security.Claims;

namespace Cocorico.Application.Mappings
{
    public sealed class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Claim, ClaimDto>()
                .ConvertUsing(c => new ClaimDto
                {
                    ClaimType = c.Type,
                    ClaimValue = c.Value,
                });

            CreateMap<ClaimDto, Claim>()
                .ConvertUsing(cd => new Claim(cd.ClaimType, cd.ClaimValue));

            CreateMap<IEnumerable<Claim>, ClaimsDto>()
                .ForMember(
                    s => s.Claims,
                    t => t.MapFrom(d => d));
        }
    }
}
