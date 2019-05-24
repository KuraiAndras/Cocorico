using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;

namespace Cocorico.Client.Domain.Services.Basket
{
    public interface IBasketService
    {
        void AddToBasket(SandwichDto sandwich);
        void RemoveFromBasket(int index);
        void EmptyBasket();
        List<SandwichDto> SandwichesInBasket { get; }
    }
}
