namespace Cocorico.Shared.Services.Price
{
    public class PriceCalculator : IPriceCalculator
    {
        public int CalculatePrice(int basePrice, int numberOfAdditions, int numberOfRemovals, int itemPrice) =>
            numberOfAdditions <= numberOfRemovals
                ? basePrice
                : basePrice + ((numberOfAdditions - numberOfRemovals) * itemPrice);
    }
}