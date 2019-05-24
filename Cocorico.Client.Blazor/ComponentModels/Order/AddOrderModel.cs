using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Client.Domain.Services.Basket;
using Cocorico.Shared.Dtos.Order;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Order
{
    public class AddOrderModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IBasketService BasketService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IOrderClient OrderHttpClient { get; set; }

        protected OrderAddDto OrderAddDto { get; } = new OrderAddDto();

        protected override Task OnInitAsync()
        {
            OrderAddDto.Sandwiches = BasketService.SandwichesInBasket;
            return base.OnInitAsync();
        }

        protected async Task Add()
        {
            try
            {
                var result = await OrderHttpClient.AddOrderAsync(OrderAddDto);

                //TODO: Go to orders
                if (result.IsSuccessfulStatusCode())
                {
                    BasketService.EmptyBasket();
                }
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        protected void DeleteSandwich(int id) => BasketService.RemoveFromBasket(id);
    }
}
