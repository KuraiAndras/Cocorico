using MediatR;
using System;

namespace Cocorico.Shared.Api.Openings
{
    public sealed class AddOpening : IRequest
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
