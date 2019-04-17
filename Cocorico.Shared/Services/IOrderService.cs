using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Shared.Services
{
    public interface IOrderService
    {
        Task<IServiceResult<IEnumerable<CustomerOrderDto>>> GetAllOrderForCustomerAsync(string customerId);
        Task<IServiceResult<IEnumerable<WorkerOrderDto>>> GetPendingOrdersForWorkerAsync();
        Task<IServiceResult> AddOrderAsync(WorkerOrderDto workerOrderDto);
        Task<IServiceResult> DeleteOrderAsync(int orderId);
    }
}
