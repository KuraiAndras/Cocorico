using Cocorico.Shared.Dtos.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.OrderService
{
    public interface IServerOrderService
    {
        Task<IEnumerable<OrderCustomerViewDto>> GetAllOrderForCustomerAsync(string customerId);
        Task<IEnumerable<OrderWorkerViewDto>> GetPendingOrdersForWorkerAsync();
        Task UpdateOrderAsync(UpdateOrderDto updateOrderDto);
        Task AddOrderAsync(OrderAddDto orderAddDto);
        Task DeleteOrderAsync(int orderId);
    }
}
