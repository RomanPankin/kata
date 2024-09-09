namespace Kata;

using System.Text.RegularExpressions;

class CheckoutSpecialRuleBuyXForY : ICheckoutSpecialRule
{
    private static readonly Regex s_RulePattern = new Regex(@"^(\d+)\s+for\s+(\d+(?:\.\d+)?)$", RegexOptions.IgnoreCase);

    public string Item { get; private set; }
    public int Amount { get; private set; }
    public decimal Price { get; private set; }

    public CheckoutSpecialRuleBuyXForY(string item, int amount, decimal price)
    {
        Item = item;
        Amount = amount;
        Price = price;
    }

    public (decimal, IEnumerable<CheckoutItem>?) ApplySpecialPrice(IEnumerable<CheckoutItem> items)
    {
        var count = items.Where(p => p.Name == Item).Count();
        if (count < Amount)
        {
            return (0, null);
        }

        var appliedTimes = count / Amount;
        var itemsToExclude = appliedTimes * Amount;
        var usedItems = new List<CheckoutItem>();

        foreach (var item in items)
        {
            if (item.Name == Item)
            {
                usedItems.Add(item);

                itemsToExclude--;
                if (itemsToExclude <= 0) break;
            }
        }

        return (appliedTimes * Price, usedItems);
    }

    public static bool TryParse(string item, string rule, out ICheckoutSpecialRule? checkoutRuleBuyXForY)
    {
        Match match = s_RulePattern.Match(rule);
        if (match.Success)
        {
            var amount = int.Parse(match.Groups[1].Value);
            var price = decimal.Parse(match.Groups[2].Value);

            checkoutRuleBuyXForY = new CheckoutSpecialRuleBuyXForY(item, amount, price);
            return true;
        }

        checkoutRuleBuyXForY = null;
        return false;
    }
}