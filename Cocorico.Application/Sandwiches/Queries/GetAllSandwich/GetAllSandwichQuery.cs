using Cocorico.Shared.Dtos.Sandwiches;
using MediatR;
using System.Collections.Generic;

namespace Cocorico.Application.Sandwiches.Queries.GetAllSandwich
{
    public sealed class GetAllSandwichQuery : IRequest<ICollection<SandwichDto>>
    {
    }
}
