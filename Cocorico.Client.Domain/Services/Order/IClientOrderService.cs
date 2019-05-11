using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Services;
using System.Collections.Generic;

namespace Cocorico.Client.Domain.Services.Order
{
    public interface IClientOrderService : IOrderService
    {
        void AddToBasket(SandwichResultDto sandwich);
        void RemoveFromBasket(int sandwichId);
        IEnumerable<SandwichResultDto> SandwichesInBasket();
    }
}
