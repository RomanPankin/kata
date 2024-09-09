namespace Kata.Tests;

using Kata;

public class CheckoutWithRulesTests
{
    private readonly Checkout _checkout;

    public CheckoutWithRulesTests()
    {
        _checkout = new Checkout(new CheckoutRule[]
        {
            new CheckoutRule("A", 50, "3 for 130"),
            new CheckoutRule("B", 30, "2 for 45"),
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
            new object[] { "AA", 100m },
            new object[] { "AAA", 130m },
            new object[] { "AAAA", 180m },
            new object[] { "AAAAA", 230m },
            new object[] { "AAAAAA", 260m },
            new object[] { "AAAB", 160m },
            new object[] { "AAABB", 175m },
            new object[] { "AAABBD", 190m },
            new object[] { "DABABA", 190m },
        };
}