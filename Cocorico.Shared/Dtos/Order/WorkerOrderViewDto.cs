﻿using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Order
{
    public class WorkerOrderViewDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;
        public ICollection<SandwichDto> Sandwiches { get; set; } = new List<SandwichDto>();
        public OrderState State { get; set; }
    }
}
