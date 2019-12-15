using AutoMapper;
using Cocorico.Domain.Entities;
using Cocorico.Shared.Dtos.Ingredient;

namespace Cocorico.Mappings
{
    public sealed class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<IngredientAddDto, Ingredient>();
            CreateMap<IngredientDto, Ingredient>();

            CreateMap<IngredientModification, IngredientModificationDto>();
            CreateMap<IngredientModificationDto, IngredientModification>();
        }
    }
}
