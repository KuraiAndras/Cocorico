using AutoMapper;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Dtos.Sandwiches;
using System.Linq;

namespace Cocorico.Application.Mappings
{
    public sealed class SandwichProfile : Profile
    {
        public SandwichProfile()
        {
            CreateMap<Sandwich, SandwichDto>()
                .ForMember(
                    d => d.Ingredients,
                    o => o.MapFrom(s => s.SandwichIngredients.Select(si => si.Ingredient)));
            CreateMap<SandwichAddDto, Sandwich>();
            CreateMap<SandwichDto, Sandwich>();
        }
    }
}
