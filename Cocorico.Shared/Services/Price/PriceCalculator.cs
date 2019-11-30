using Cocorico.Shared.Exceptions;

namespace Cocorico.Shared.Services.Price
{
    public class PriceCalculator : IPriceCalculator
    {
        public int CalculatePrice(int basePrice, int numberOfAdditions, int numberOfRemovals, int itemPrice)
        {
            if (numberOfAdditions <= numberOfRemovals) return basePrice;

            if (numberOfAdditions > numberOfRemovals) return basePrice + ((numberOfAdditions - numberOfRemovals) * itemPrice);

            throw new UnexpectedException("Math is broken apparently");
        }
    }
}