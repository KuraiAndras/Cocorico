using Cocorico.Shared.Dtos.Sandwich;

namespace Cocorico.Application.Sandwiches.Queries.GetSandwich
{
    public class GetSandwichQuery : QueryDtoBase<int, SandwichDto>
    {
        public GetSandwichQuery(int dto) : base(dto)
        {
        }
    }
}
