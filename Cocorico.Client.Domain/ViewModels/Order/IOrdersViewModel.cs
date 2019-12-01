using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Order
{
    public interface IOrdersViewModel
    {
        List<WorkerOrderViewDto> Orders { get; }

        Task InitializeAsync();
        Task UpdateStateAsync(int orderId, OrderState newState);

        event Action OrdersChanged;
    }
}
