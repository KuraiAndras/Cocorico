using Cocorico.Shared.Dtos.Ingredients;
using Cocorico.Shared.Dtos.Sandwiches;
using Cocorico.Shared.Entities;
using System;
using System.Collections.Generic;

namespace Cocorico.Shared.Api.Orders
{
    public class CustomerViewOrderDto
    {
        public int Id { get; set; }
        public ICollection<SandwichDto> Sandwiches { get; set; } = new List<SandwichDto>();
        public int Price { get; set; }
        public DateTime Time { get; set; }
        public OrderState State { get; set; }
        public IDictionary<SandwichDto, ICollection<IngredientModificationDto>> SandwichModifications { get; set; } = new Dictionary<SandwichDto, ICollection<IngredientModificationDto>>();
    }
}
