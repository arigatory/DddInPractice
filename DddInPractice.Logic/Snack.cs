namespace DddInPractice.Logic;

public class Snack : AggregateRoot
{
    public static readonly Snack None = new Snack(0, "Пусто");
    public static readonly Snack Chocolate = new Snack(1, "Шоколад");
    public static readonly Snack Soda = new Snack(2, "Лимонад");
    public static readonly Snack Gum = new Snack(3, "Жвачка");
    
    public virtual string Name { get; protected set; }

    protected Snack()
    {

    }

    private Snack(long id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}
