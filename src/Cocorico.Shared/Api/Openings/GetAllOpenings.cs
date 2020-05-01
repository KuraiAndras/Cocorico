using MediatR;
using System.Collections.Generic;

namespace Cocorico.Shared.Api.Openings
{
    public sealed class GetAllOpenings : IRequest<ICollection<OpeningDto>>
    {
    }
}
