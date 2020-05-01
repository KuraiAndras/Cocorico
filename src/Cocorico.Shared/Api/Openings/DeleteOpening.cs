using MediatR;

namespace Cocorico.Shared.Api.Openings
{
    public sealed class DeleteOpening : IRequest
    {
        public int Id { get; set; }
    }
}
