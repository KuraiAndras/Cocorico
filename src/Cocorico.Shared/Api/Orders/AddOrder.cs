using Cocorico.Shared.Dtos.Ingredients;
using Cocorico.Shared.Dtos.Sandwiches;
using MediatR;
using System.Collections.Generic;

namespace Cocorico.Shared.Api.Orders
{
    public class AddOrder : IRequest
    {
        public string UserId { get; set; } = string.Empty;
        public IList<SandwichDto> Sandwiches { get; set; } = new List<SandwichDto>();
        public string CustomerId { get; set; } = string.Empty;
        public IDictionary<SandwichDto, ICollection<IngredientModificationDto>> SandwichModifications { get; set; } = new Dictionary<SandwichDto, ICollection<IngredientModificationDto>>();
    }
}
