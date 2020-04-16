using Cocorico.Shared.Dtos.Ingredients;
using MediatR;
using System.Collections.Generic;

namespace Cocorico.Application.Ingredients.Queries.GetAllIngredients
{
    public sealed class GetAllIngredientsQuery : IRequest<ICollection<IngredientDto>>
    {
    }
}
