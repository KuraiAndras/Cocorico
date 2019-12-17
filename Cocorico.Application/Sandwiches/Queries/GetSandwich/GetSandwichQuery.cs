using Cocorico.Shared.Dtos.Sandwich;
using MediatR;

namespace Cocorico.Application.Sandwiches.Queries.GetSandwich
{
    public class GetSandwichQuery : IRequest<SandwichDto>
    {
        public GetSandwichQuery(int id) => Id = id;

        public int Id { get; }
    }
}
