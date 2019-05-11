using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.Services.Order
{
    public interface IClientOrderService
    {
        Task<IServiceResult<IEnumerable<OrderCustomerViewDto>>> GetAllOrderForCustomerAsync(string customerId);
        Task<IServiceResult<IEnumerable<OrderWorkerViewDto>>> GetPendingOrdersForWorkerAsync();
        Task<IServiceResult> UpdateOrderAsync(UpdateOrderDto updateOrderDto);
        Task<IServiceResult> AddOrderAsync(OrderAddDto orderAddDto);
        Task<IServiceResult> DeleteOrderAsync(int orderId);

        void AddToBasket(SandwichResultDto sandwich);
        void RemoveFromBasket(int sandwichId);
        IEnumerable<SandwichResultDto> SandwichesInBasket();
    }
}
