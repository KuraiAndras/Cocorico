using System;

namespace Cocorico.Shared.Helpers
{
    public enum OrderState
    {
        OrderPlaced,
        InTheOven,
        Delivered,
    }

    public static class OrderStateExtension
    {
        public static string ToString(this OrderState orderState)
        {
            switch (orderState)
            {
                case OrderState.OrderPlaced:
                    return "Order Placed";
                case OrderState.InTheOven:
                    return "In The Oven";
                case OrderState.Delivered:
                    return "Delivered";
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderState), orderState, null);
            }
        }
    }
}
