using MediatR;
using System.Collections.Generic;

namespace Cocorico.Shared.Api.Ingredients
{
    public sealed class GetAllIngredients : IRequest<ICollection<IngredientDto>>
    {
    }
}
