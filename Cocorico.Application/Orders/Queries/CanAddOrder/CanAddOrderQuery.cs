using MediatR;
using System;

namespace Cocorico.Application.Orders.Queries.CanAddOrder
{
    public sealed class CanAddOrderQuery : IRequest<bool>
    {
        public CanAddOrderQuery(DateTime time) => Time = time;

        public DateTime Time { get; }
    }
}
