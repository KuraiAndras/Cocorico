using System;

namespace Cocorico.Application.Orders.Queries.CanAddOrder
{
    // TODO: explicit dto class
    public sealed class CanAddOrderQuery : QueryDtoBase<DateTime, bool>
    {
        public CanAddOrderQuery(DateTime dto) : base(dto)
        {
        }
    }
}
