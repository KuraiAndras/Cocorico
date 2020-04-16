using AutoMapper;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Dtos.User;

namespace Cocorico.Application.Mappings
{
    public sealed class CocoricoUserProfile : Profile
    {
        public CocoricoUserProfile()
        {
            CreateMap<RegisterDetails, CocoricoUser>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Name));
            CreateMap<CocoricoUser, UserForAdminPage>();
        }
    }
}