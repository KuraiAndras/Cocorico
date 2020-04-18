using AutoMapper;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Sandwiches;
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
            CreateMap<AddSandwich, Sandwich>();
            CreateMap<SandwichDto, Sandwich>();
        }
    }
}
