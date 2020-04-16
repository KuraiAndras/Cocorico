using AutoMapper;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Dtos.Ingredients;

namespace Cocorico.Application.Mappings
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
