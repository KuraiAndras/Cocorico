using Cocorico.Shared.Dtos.Opening;
using MediatR;
using System.Collections.Generic;

namespace Cocorico.Application.Openings.Queries.GetAllOpenings
{
    public sealed class GetAllOpeningsQuery : IRequest<ICollection<OpeningDto>>
    {
    }
}
