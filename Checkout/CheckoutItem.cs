namespace Kata;

public class CheckoutItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public CheckoutItem(int id, string name)
    {
        Id = id;
        Name = name;
    }
}