namespace DddInPractice.Logic;
using static DddInPractice.Logic.Money;

public class SnackMachine : Entity
{
    public virtual Money MoneyInside { get; protected set; }
    public virtual Money MoneyInTransaction { get; protected set; }
    public virtual IList<Slot> Slots { get; protected set; }

    public SnackMachine()
    {
        MoneyInside = None;
        MoneyInTransaction = None;
        Slots = new List<Slot>
        {
            new Slot(snackMachine: this,
            position: 1,
            snack: null,
            quantity: 0,
            price: 0),
            new Slot(snackMachine: this,
            position: 2,
            snack: null,
            quantity: 0,
            price: 0),
            new Slot(snackMachine: this,
            position: 3,
            snack: null,
            quantity: 0,
            price: 0)
        };
    }

    public virtual void InsertMoney(Money money)
    {
        Money[] notes = { TenRub, FiftyRub, HundredRub, FiveHundredRub, ThousandRub, FiveThousandRub };
        if (!notes.Contains(money))
            throw new InvalidOperationException();

        MoneyInTransaction += money;
    }

    public virtual void ReturnMoney()
    {
        MoneyInTransaction = None;
    }

    public virtual void BuySnack(int position = 0)
    {
        Slot slot = Slots.Single(x=>x.Position == position);
        slot.Quantity--;

        MoneyInside += MoneyInTransaction;
        MoneyInTransaction = None;
    }

    public virtual void LoadSnacks(int position, Snack snack, int quantity, int price)
    {
        Slot slot = Slots.Single(x => x.Position == position);
        slot.Snack = snack;
        slot.Quantity = quantity;
        slot.Price = price;
    }
}
