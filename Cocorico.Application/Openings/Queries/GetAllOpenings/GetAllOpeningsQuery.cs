using Cocorico.Shared.Dtos.Openings;
using MediatR;
using System.Collections.Generic;

namespace Cocorico.Application.Openings.Queries.GetAllOpenings
{
    public sealed class GetAllOpeningsQuery : IRequest<ICollection<OpeningDto>>
    {
    }
}
