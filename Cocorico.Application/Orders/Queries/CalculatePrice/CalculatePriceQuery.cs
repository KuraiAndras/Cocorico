﻿using Cocorico.Shared.Dtos.Orders;

namespace Cocorico.Application.Orders.Queries.CalculatePrice
{
    public sealed class CalculatePriceQuery : QueryDtoBase<AddOrderDto, int>
    {
        public CalculatePriceQuery(AddOrderDto dto)
            : base(dto)
        {
        }
    }
}
