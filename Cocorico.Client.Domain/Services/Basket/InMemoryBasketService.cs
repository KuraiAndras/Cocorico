using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;

namespace Cocorico.Client.Domain.Services.Basket
{
    public class InMemoryBasketService : IBasketService
    {
        public void AddToBasket(SandwichResultDto sandwich) => SandwichesInBasket.Add(sandwich);

        public void RemoveFromBasket(int index) => SandwichesInBasket.RemoveAt(index);
        public void EmptyBasket() => SandwichesInBasket.Clear();

        public List<SandwichResultDto> SandwichesInBasket { get; } = new List<SandwichResultDto>();
    }
}
