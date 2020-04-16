using AutoMapper;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Dtos.Orders;
using System.Linq;

namespace Cocorico.Application.Mappings
{
    public sealed class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, WorkerOrderViewDto>()
                .ForMember(
                    d => d.UserName,
                    o => o.MapFrom(s => s.CocoricoUser.Name))
                .ForMember(
                    d => d.Sandwiches,
                    o => o.MapFrom(s => s.SandwichOrders.Select(so => so.Sandwich)));
            CreateMap<Order, CustomerViewOrderDto>()
                .ForMember(
                    d => d.Sandwiches,
                    o => o.MapFrom(s => s.SandwichOrders.Select(so => so.Sandwich)));
        }
    }
}