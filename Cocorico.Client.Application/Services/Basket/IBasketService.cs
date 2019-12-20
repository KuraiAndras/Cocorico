using System.Collections.Generic;
using Cocorico.Shared.Dtos.Sandwich;

namespace Cocorico.Client.Application.Services.Basket
{
    public interface IBasketService
    {
        void AddToBasket(SandwichDto sandwich);
        void RemoveFromBasket(int index);
        void EmptyBasket();
        List<SandwichDto> SandwichesInBasket { get; }
    }
}
