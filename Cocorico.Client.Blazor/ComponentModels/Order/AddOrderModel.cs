using Cocorico.Client.Domain.Services.Order;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Order
{
    public class AddOrderModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IClientOrderService OrderService { get; set; }

        protected OrderAddDto OrderAddDto { get; } = new OrderAddDto();

        protected override Task OnInitAsync()
        {
            OrderAddDto.Sandwiches = OrderService.SandwichesInBasket();
            return base.OnInitAsync();
        }

        protected async Task Add()
        {
            var result = await OrderService.AddOrderAsync(OrderAddDto);
            switch (result)
            {
                case Success _:
                    //TODO: Go to orders
                    break;
            }
        }
    }
}
