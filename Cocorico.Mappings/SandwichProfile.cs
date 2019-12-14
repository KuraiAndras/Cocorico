using System.Linq;
using AutoMapper;
using Cocorico.Domain.Entities;
using Cocorico.Shared.Dtos.Sandwich;

namespace Cocorico.Mappings
{
    public sealed class SandwichProfile : Profile
    {
        public SandwichProfile()
        {
            CreateMap<Sandwich, SandwichDto>()
                .ForMember(d => d.Ingredients,
                    o => o.MapFrom(s => s.SandwichIngredients.Select(si => si.Ingredient)));
            CreateMap<SandwichAddDto, Sandwich>();
            CreateMap<SandwichDto, Sandwich>();
        }
    }
}