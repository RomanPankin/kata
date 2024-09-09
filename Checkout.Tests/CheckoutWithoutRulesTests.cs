namespace Kata.Tests;

using Kata;

public class CheckoutWithoutRulesTests
{
    private readonly Checkout _checkout;

    public CheckoutWithoutRulesTests()
    {
        _checkout = new Checkout(new CheckoutRule[]
        {
            new CheckoutRule("A", 50),
            new CheckoutRule("B", 30),
            new CheckoutRule("C", 20),
            new CheckoutRule("D", 15)
        });
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void TotalCostWithoutSpecialRules(string items, decimal expectedCost)
    {
        Array.ForEach(items.Select(x => x.ToString()).ToArray(), item => _checkout.Scan(item));
        Assert.Equal(expectedCost, _checkout.CalculateTotalCost());
    }

    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { "", 0m },
            new object[] { "A", 50m },
            new object[] { "B", 30m },
            new object[] { "C", 20m },
            new object[] { "D", 15m },
            new object[] { "AB", 80m },
            new object[] { "CDBA", 115m },
            new object[] { "AA", 100m },
        };
}