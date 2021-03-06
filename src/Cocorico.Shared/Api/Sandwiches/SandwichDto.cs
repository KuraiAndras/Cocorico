﻿using Cocorico.Shared.Api.Ingredients;
using System.Collections.Generic;

namespace Cocorico.Shared.Api.Sandwiches
{
    public class SandwichDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public ICollection<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
    }
}
