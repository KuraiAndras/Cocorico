using Cocorico.Shared.Api.Ingredients;
using MediatR;
using System.Collections.Generic;

namespace Cocorico.Shared.Api.Sandwiches
{
    public class AddSandwich : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public ICollection<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
    }
}
