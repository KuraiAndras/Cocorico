using Cocorico.Shared.Dtos.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Shared.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderCustomerViewDto>> GetAllOrderForCustomerAsync(string customerId);
        Task<IEnumerable<OrderWorkerViewDto>> GetPendingOrdersForWorkerAsync();
        Task UpdateOrderAsync(UpdateOrderDto updateOrderDto);
        Task AddOrderAsync(OrderAddDto orderAddDto);
        Task DeleteOrderAsync(int orderId);
    }
}
