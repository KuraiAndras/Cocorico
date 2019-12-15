using Cocorico.Domain.Entities;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Sandwich;
using System;
using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Order
{
    public class WorkerOrderViewDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public ICollection<SandwichDto> Sandwiches { get; set; } = new List<SandwichDto>();
        public int RotatingId { get; set; }
        public DateTime Time { get; set; }
        public OrderState State { get; set; }
        public IDictionary<SandwichDto, ICollection<IngredientModificationDto>> SandwichModifications { get; set; } = new Dictionary<SandwichDto, ICollection<IngredientModificationDto>>();
    }
}
