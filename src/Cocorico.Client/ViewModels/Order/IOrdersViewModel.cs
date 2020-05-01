using Cocorico.Shared.Api.Orders;
using Cocorico.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Order
{
    public interface IOrdersViewModel
    {
        event Action OrdersChanged;

        List<WorkerOrderViewDto> Orders { get; }

        Task InitializeAsync();
        Task UpdateStateAsync(int orderId, OrderState newState);
    }
}
