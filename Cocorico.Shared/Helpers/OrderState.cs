using System;

namespace Cocorico.Shared.Helpers
{
    public enum OrderState
    {
        OrderPlaced,
        InTheOven,
        ToBeDelivered,
        Delivered,
    }

    public static class OrderStateExtension
    {
        public static string ToString(this OrderState orderState)
        {
            return orderState switch
            {
                OrderState.OrderPlaced => "Order Placed",
                OrderState.InTheOven => "In The Oven",
                OrderState.ToBeDelivered => "To Be Delivered",
                OrderState.Delivered => "Delivered",
                _ => throw new ArgumentOutOfRangeException(nameof(orderState), orderState, null)
            };
        }
    }
}
