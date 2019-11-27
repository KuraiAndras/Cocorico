using Cocorico.Shared.Dtos.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.OrderService
{
    public interface IServerOrderService
    {
        Task<ICollection<CustomerViewOrderDto>> GetAllOrderForCustomerAsync(string customerId);
        Task<ICollection<WorkerOrderViewDto>> GetPendingOrdersForWorkerAsync();
        Task UpdateOrderAsync(UpdateOrderDto updateOrderDto);
        Task<int> AddOrderAsync(AddOrderDto addOrderDto);
        Task DeleteOrderAsync(int orderId);
    }
}
