using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Order
{
    public class UpdateOrdersModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IOrderClient OrderHttpClient { get; set; }

        protected IReadOnlyCollection<OrderWorkerViewDto> Orders { get; private set; } = new List<OrderWorkerViewDto>();

        protected override async Task OnInitAsync() => await LoadOrdersAsync();

        protected async Task UpdateStateAsync(int orderId, OrderState newState)
        {
            try
            {
                var fileResponse = await OrderHttpClient.UpdateOrderAsync(new UpdateOrderDto
                {
                    OrderId = orderId,
                    State = newState,
                });

                //TODO: Handle fail
                if (fileResponse.IsSuccessfulStatusCode()) await LoadOrdersAsync();
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        private async Task LoadOrdersAsync()
        {
            try
            {
                var pendingOrders = await OrderHttpClient.GetPendingOrdersForWorkerAsync();

                Orders = pendingOrders.ToList();
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}
