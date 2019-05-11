using Cocorico.Client.Domain.Services.Order;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Order
{
    public class UpdateOrdersModel : ComponentBase
    {
        [Inject] private IClientOrderService OrderService { get; set; }

        protected IReadOnlyCollection<OrderWorkerViewDto> Orders { get; private set; } = new List<OrderWorkerViewDto>();

        protected override async Task OnInitAsync() => await LoadOrdersAsync();

        protected async Task UpdateStateAsync(int orderId, OrderState newState)
        {
            var result = await OrderService.UpdateOrderAsync(new UpdateOrderDto
            {
                OrderId = orderId,
                State = newState,
            });

            if (result is Success) await LoadOrdersAsync();
        }

        private async Task LoadOrdersAsync()
        {
            var result = await OrderService.GetPendingOrdersForWorkerAsync();

            //TODO: Handle fail
            switch (result)
            {
                case Success<IEnumerable<OrderWorkerViewDto>> success:
                    Orders = success.Data.ToList();
                    break;
            }
        }
    }
}
