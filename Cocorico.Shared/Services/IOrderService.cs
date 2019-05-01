using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cocorico.Shared.Helpers;

namespace Cocorico.Shared.Services
{
    public interface IOrderService
    {
        Task<IServiceResult<IEnumerable<OrderCustomerViewDto>>> GetAllOrderForCustomerAsync(string customerId);
        Task<IServiceResult<IEnumerable<OrderWorkerViewDto>>> GetPendingOrdersForWorkerAsync();
        Task<IServiceResult> UpdateOrderAsync(UpdateOrderDto updateOrderDto);
        Task<IServiceResult> AddOrderAsync(OrderAddDto orderAddDto);
        Task<IServiceResult> DeleteOrderAsync(int orderId);
    }
}
