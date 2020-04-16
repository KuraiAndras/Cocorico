using Cocorico.Shared.Dtos.Ingredients;
using Cocorico.Shared.Dtos.Sandwiches;
using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Orders
{
    public class AddOrderDto
    {
        public string UserId { get; set; } = string.Empty;
        public IList<SandwichDto> Sandwiches { get; set; } = new List<SandwichDto>();
        public string CustomerId { get; set; } = string.Empty;
        public IDictionary<SandwichDto, ICollection<IngredientModificationDto>> SandwichModifications { get; set; } = new Dictionary<SandwichDto, ICollection<IngredientModificationDto>>();
    }
}
