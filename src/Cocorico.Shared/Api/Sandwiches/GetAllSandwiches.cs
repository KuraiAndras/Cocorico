using MediatR;
using System.Collections.Generic;

namespace Cocorico.Shared.Api.Sandwiches
{
    public sealed class GetAllSandwiches : IRequest<ICollection<SandwichDto>>
    {
    }
}
