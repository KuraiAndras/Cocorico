using Cocorico.Shared.Dtos.Ingredients;
using Cocorico.Shared.Dtos.Sandwiches;
using MediatR;
using System.Collections.Generic;

namespace Cocorico.Shared.Api.Orders
{
    public sealed class CalculatePrice : IRequest<int>
    {
        public IList<SandwichDto> Sandwiches { get; set; } = new List<SandwichDto>();
        public IDictionary<SandwichDto, ICollection<IngredientModificationDto>> SandwichModifications { get; set; } = new Dictionary<SandwichDto, ICollection<IngredientModificationDto>>();
    }
}
