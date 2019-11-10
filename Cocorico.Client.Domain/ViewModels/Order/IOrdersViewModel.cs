using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Order
{
    public interface IOrdersViewModel
    {
        IReadOnlyCollection<OrderWorkerViewDto> Orders { get; }

        Task UpdateStateAsync(int orderId, OrderState newState);
        Task LoadOrdersAsync();
    }
}
