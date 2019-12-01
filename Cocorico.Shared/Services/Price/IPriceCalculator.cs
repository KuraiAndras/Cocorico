namespace Cocorico.Shared.Services.Price
{
    public interface IPriceCalculator
    {
        int CalculatePrice(int basePrice, int numberOfAdditions, int numberOfRemovals, int itemPrice);
    }
}
