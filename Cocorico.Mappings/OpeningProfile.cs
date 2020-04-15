using AutoMapper;
using Cocorico.Domain.Entities;
using Cocorico.Shared.Dtos.Openings;

namespace Cocorico.Mappings
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
            CreateMap<AddOpeningDto, Opening>();
        }
    }
}
