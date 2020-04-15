using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Orders;
using Cocorico.Shared.Entities;

namespace Cocorico.Client.Application.ViewModels.Order
{
    public interface IOrdersViewModel
    {
        event Action OrdersChanged;

        List<WorkerOrderViewDto> Orders { get; }

        Task InitializeAsync();
        Task UpdateStateAsync(int orderId, OrderState newState);
    }
}
