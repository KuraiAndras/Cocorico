using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;

namespace Cocorico.Client.Domain.Services.Basket
{
    public class InMemoryBasketService : IBasketService
    {
        private readonly List<SandwichResultDto> _basket = new List<SandwichResultDto>();

        public void AddToBasket(SandwichResultDto sandwich) => _basket.Add(sandwich);

        public void RemoveFromBasket(int sandwichId) => _basket.Remove(_basket.Find(s => s.Id == sandwichId));

        public IEnumerable<SandwichResultDto> SandwichesInBasket() => _basket;
    }
}
