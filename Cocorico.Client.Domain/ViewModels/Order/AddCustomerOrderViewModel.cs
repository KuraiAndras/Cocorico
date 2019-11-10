using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Client.Domain.Services.Basket;
using Cocorico.Shared.Dtos.Order;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Order
{
    public class AddCustomerOrderViewModel : IAddCustomerOrderViewModel
    {
        private readonly IBasketService _basketService;
        private readonly IOrderClient _orderClient;

        public OrderAddDto OrderAddDto { get; }

        public AddCustomerOrderViewModel(IBasketService basketService, IOrderClient orderClient)
        {
            _basketService = basketService;
            _orderClient = orderClient;
            OrderAddDto = new OrderAddDto { Sandwiches = _basketService.SandwichesInBasket };
        }

        public async Task AddAsync()
        {
            try
            {
                var result = await _orderClient.AddOrderAsync(OrderAddDto);

                //TODO: Go to orders
                if (result.IsSuccessfulStatusCode())
                {
                    _basketService.EmptyBasket();
                }
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        public void DeleteSandwich(int id) => _basketService.RemoveFromBasket(id);
    }
}