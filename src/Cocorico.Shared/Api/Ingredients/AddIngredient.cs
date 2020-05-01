using MediatR;

namespace Cocorico.Shared.Api.Ingredients
{
    public class AddIngredient : IRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
