using System.Collections.Generic;
using Cocorico.Shared.Dtos.Sandwiches;

namespace Cocorico.Client.Application.Services.Basket
{
    public class InMemoryBasketService : IBasketService
    {
        public List<SandwichDto> SandwichesInBasket { get; } = new List<SandwichDto>();

        public void AddToBasket(SandwichDto sandwich) => SandwichesInBasket.Add(sandwich);

        public void RemoveFromBasket(int index) => SandwichesInBasket.RemoveAt(index);
        public void EmptyBasket() => SandwichesInBasket.Clear();
    }
}
