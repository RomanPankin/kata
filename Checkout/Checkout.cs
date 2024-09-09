namespace Kata;

public class Checkout
{
    private readonly IDictionary<string, CheckoutRule> _priceMapping = new Dictionary<string, CheckoutRule>();
    private readonly IList<CheckoutRule> _specialRules = new List<CheckoutRule>();
    private readonly IList<CheckoutItem> _items = new List<CheckoutItem>();
    private int _ids = 0;

    public Checkout()
    {
    }

    public Checkout(IEnumerable<CheckoutRule> rules)
    {
        AddRules(rules);
    }

    public void AddRules(IEnumerable<CheckoutRule> rules)
    {
        foreach (var rule in rules)
        {
            if (rule.SpecialPrice != null)
            {
                _specialRules.Add(rule);
            }

            _priceMapping.Add(rule.Item, rule);
        }
    }

    public void Scan(string item)
    {
        _items.Add(new CheckoutItem(++_ids, item));
    }

    public decimal CalculateTotalCost()
    {
        var unusedItemIds = new HashSet<int>();

        // special rules
        var costWithSpecialPrice = 0m;
        foreach (var rule in _specialRules)
        {
            var itemToUse = _items.Where(x => !unusedItemIds.Contains(x.Id));

            var (costPerRule, usedItemsPerRule) = rule.ApplySpecialPrice(itemToUse);
            if (usedItemsPerRule != null)
            {
                costWithSpecialPrice += costPerRule;
                unusedItemIds.UnionWith(usedItemsPerRule.Select(x => x.Id));
            }
        }

        // cost of other items
        var costOfOtherItems = _items.Where(x => !unusedItemIds.Contains(x.Id)).Sum(x => _priceMapping[x.Name].UnitPrice);

        // total cost
        var totalCost = costWithSpecialPrice + costOfOtherItems;

        return totalCost;
    }
}
