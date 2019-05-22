using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;

namespace Cocorico.Client.Domain.Services.Basket
{
    public interface IBasketService
    {
        void AddToBasket(SandwichResultDto sandwich);
        void RemoveFromBasket(int index);
        void EmptyBasket();
        List<SandwichResultDto> SandwichesInBasket { get; }
    }
}
