using AutoMapper;
using Cocorico.Domain.Entities;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Dtos.User;

namespace Cocorico.Mappings
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