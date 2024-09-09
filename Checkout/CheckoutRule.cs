namespace Kata;

public class CheckoutRule
{
    public string Item { get; private set; }
    public decimal UnitPrice { get; private set; }
    public ICheckoutSpecialRule? SpecialPrice { get; private set; }

    public CheckoutRule(string item, decimal unitPrice, string? specialPrice = null)
    {
        Item = item;
        UnitPrice = unitPrice;
        SpecialPrice = ParseSpecialPrice(item, specialPrice);
    }

    public (decimal, IEnumerable<CheckoutItem>?) ApplySpecialPrice(IEnumerable<CheckoutItem> items)
    {
        return SpecialPrice?.ApplySpecialPrice(items) ?? (0, null);
    }

    private ICheckoutSpecialRule? ParseSpecialPrice(string item, string? specialPriceText)
    {
        if (string.IsNullOrEmpty(specialPriceText))
        {
            return null;
        }

        ICheckoutSpecialRule? specialPrice = null;
        CheckoutSpecialRuleBuyXForY.TryParse(item, specialPriceText, out specialPrice);

        return specialPrice;
    }
}