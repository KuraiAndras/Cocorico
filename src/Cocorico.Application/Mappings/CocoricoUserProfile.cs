using AutoMapper;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Users;

namespace Cocorico.Application.Mappings
{
    public sealed class CocoricoUserProfile : Profile
    {
        public CocoricoUserProfile()
        {
            CreateMap<RegisterUser, CocoricoUser>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Name));
            CreateMap<CocoricoUser, UserForAdminPage>();
        }
    }
}
