using MediatR;

namespace Cocorico.Shared.Api.Sandwiches
{
    public class GetSandwich : IRequest<SandwichDto>
    {
        public int Id { get; set; }
    }
}
