namespace DddInPractice.Logic;

public sealed class SnackPile : ValueObject<SnackPile>
{
    public Snack Snack { get; }
    public int Quantity { get; }
    public int Price { get; }

    public SnackPile()
    {

    }

    public SnackPile(Snack snack, int quantity, int price) : this()
    {
        if (quantity < 0)
            throw new InvalidOperationException();
        if (price < 0)
            throw new InvalidOperationException();

        Snack = snack;
        Quantity = quantity;
        Price = price;
    }

    protected override bool EqualsCore(SnackPile other)
    {
        return Snack == other.Snack
            && Quantity == other.Quantity
            && Price == other.Price;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = Snack.GetHashCode();
            hashCode = (hashCode * 397) ^ Quantity;
            hashCode = (hashCode * 397) ^ Price;
            return hashCode;
        }
    }

    internal SnackPile SubtractOne()
    {
        return new SnackPile(Snack, Quantity - 1, Price);
    }
}
