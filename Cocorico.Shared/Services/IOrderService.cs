using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Shared.Services
{
    public interface IOrderService
    {
        Task<IServiceResult<IEnumerable<OrderCustomerViewDto>>> GetAllOrderForCustomerAsync(string customerId);
        Task<IServiceResult<IEnumerable<OrderWorkerViewDto>>> GetPendingOrdersForWorkerAsync();
        Task<IServiceResult> AddOrderAsync(OrderAddDto workerOrderDto);
        Task<IServiceResult> DeleteOrderAsync(int orderId);
    }
}
