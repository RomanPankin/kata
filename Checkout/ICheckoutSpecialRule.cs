namespace Kata;

public interface ICheckoutSpecialRule
{
    (decimal, IEnumerable<CheckoutItem>?) ApplySpecialPrice(IEnumerable<CheckoutItem> items);
}