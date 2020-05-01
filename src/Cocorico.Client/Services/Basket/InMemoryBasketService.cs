using Cocorico.Shared.Api.Sandwiches;
using System.Collections.Generic;

namespace Cocorico.Client.Services.Basket
{
    public class InMemoryBasketService : IBasketService
    {
        public List<SandwichDto> SandwichesInBasket { get; } = new List<SandwichDto>();

        public void AddToBasket(SandwichDto sandwich) => SandwichesInBasket.Add(sandwich);

        public void RemoveFromBasket(int index) => SandwichesInBasket.RemoveAt(index);
        public void EmptyBasket() => SandwichesInBasket.Clear();
    }
}
