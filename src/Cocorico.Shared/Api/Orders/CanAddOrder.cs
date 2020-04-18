using MediatR;
using System;

namespace Cocorico.Shared.Api.Orders
{
    public sealed class CanAddOrder : IRequest<bool>
    {
        public DateTime RequestTime { get; set; }
    }
}
