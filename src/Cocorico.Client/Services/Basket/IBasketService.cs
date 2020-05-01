using Cocorico.Shared.Api.Sandwiches;
using System.Collections.Generic;

namespace Cocorico.Client.Services.Basket
{
    public interface IBasketService
    {
        List<SandwichDto> SandwichesInBasket { get; }

        void AddToBasket(SandwichDto sandwich);
        void RemoveFromBasket(int index);
        void EmptyBasket();
    }
}
