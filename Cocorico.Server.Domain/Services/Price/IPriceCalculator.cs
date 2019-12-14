namespace Cocorico.Server.Domain.Services.Price
{
    public interface IPriceCalculator
    {
        int CalculatePrice(int basePrice, int numberOfAdditions, int numberOfRemovals, int itemPrice);
    }
}
