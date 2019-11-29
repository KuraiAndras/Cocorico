using AutoMapper;
using Cocorico.DAL.Models.Entities;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Opening;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Dtos.User;

namespace Cocorico.Mappings
{
    public class CocoricoProfile : Profile
    {
        public CocoricoProfile()
        {
            CreateMap<RegisterDetails, CocoricoUser>();
            CreateMap<CocoricoUser, UserForAdminPage>();

            CreateMap<Ingredient, IngredientDto>();
            CreateMap<IngredientAddDto, Ingredient>();
            CreateMap<IngredientDto, Ingredient>();

            CreateMap<Order, WorkerOrderViewDto>()
                .ForMember(
                    d => d.UserName,
                    o => o.MapFrom(s => s.CocoricoUser.Name))
                .ForMember(
                    d => d.Sandwiches,
                    o => o.MapFrom(s => s.Sandwiches()));
            CreateMap<Order, CustomerViewOrderDto>()
                .ForMember(
                    d => d.Sandwiches,
                    o => o.MapFrom(s => s.Sandwiches()));

            CreateMap<Sandwich, SandwichDto>()
                .ForMember(d => d.Ingredients,
                    o => o.MapFrom(s => s.Ingredients()));
            CreateMap<SandwichAddDto, Sandwich>();
            CreateMap<SandwichDto, Sandwich>();

            CreateMap<Opening, OpeningDto>();
            CreateMap<OpeningDto, Opening>();
            CreateMap<AddOpeningDto, Opening>();
        }
    }
}
