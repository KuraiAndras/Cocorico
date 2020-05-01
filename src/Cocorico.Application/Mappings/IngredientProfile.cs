using AutoMapper;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Ingredients;

namespace Cocorico.Application.Mappings
{
    public sealed class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<AddIngredient, Ingredient>();
            CreateMap<IngredientDto, Ingredient>();

            CreateMap<IngredientModification, IngredientModificationDto>();
            CreateMap<IngredientModificationDto, IngredientModification>();
        }
    }
}
