using Cocorico.Shared.Dtos.Sandwich;
using MediatR;
using System.Collections.Generic;

namespace Cocorico.Application.Sandwiches.Queries.GetAllSandwich
{
    public sealed class GetAllSandwichQuery : IRequest<ICollection<SandwichDto>>
    {
    }
}
