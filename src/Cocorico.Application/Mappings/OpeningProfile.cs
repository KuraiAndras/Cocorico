using AutoMapper;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Openings;

namespace Cocorico.Application.Mappings
{
    public sealed class OpeningProfile : Profile
    {
        public OpeningProfile()
        {
            CreateMap<Opening, OpeningDto>()
                .ForMember(
                    d => d.NumberOfOrders,
                    o => o.MapFrom(s => s.Orders.Count));
            CreateMap<OpeningDto, Opening>();
            CreateMap<AddOpening, Opening>();
        }
    }
}
