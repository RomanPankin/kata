namespace Kata.Tests;

using Kata;

public class CheckoutBasicUnitTests
{
    private readonly Checkout _checkout;

    public CheckoutBasicUnitTests()
    {
        _checkout = new Checkout();
    }

    [Fact]
    public void EmptyCheckout()
    {
        Assert.Equal(0, _checkout.CalculateTotalCost());
    }
}