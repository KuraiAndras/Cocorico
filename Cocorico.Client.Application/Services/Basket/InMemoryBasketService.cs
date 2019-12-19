using System.Collections.Generic;
using Cocorico.Shared.Dtos.Sandwich;

namespace Cocorico.Client.Application.Services.Basket
{
    public class InMemoryBasketService : IBasketService
    {
        public void AddToBasket(SandwichDto sandwich) => SandwichesInBasket.Add(sandwich);

        public void RemoveFromBasket(int index) => SandwichesInBasket.RemoveAt(index);
        public void EmptyBasket() => SandwichesInBasket.Clear();

        public List<SandwichDto> SandwichesInBasket { get; } = new List<SandwichDto>();
    }
}
