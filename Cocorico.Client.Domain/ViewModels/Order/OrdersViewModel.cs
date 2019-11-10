using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Order
{
    public class OrdersViewModel : IOrdersViewModel
    {
        private readonly IOrderClient _orderClient;

        public IReadOnlyCollection<OrderWorkerViewDto> Orders { get; private set; }

        public OrdersViewModel(IOrderClient orderClient)
        {
            _orderClient = orderClient;
            Orders = new List<OrderWorkerViewDto>();
        }

        public async Task UpdateStateAsync(int orderId, OrderState newState)
        {
            try
            {
                var fileResponse = await _orderClient.UpdateOrderAsync(new UpdateOrderDto
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

        public async Task LoadOrdersAsync()
        {
            try
            {
                var pendingOrders = await _orderClient.GetPendingOrdersForWorkerAsync();

                Orders = pendingOrders.ToList();
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}
