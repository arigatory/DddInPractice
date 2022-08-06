namespace DddInPractice.Logic;
using static DddInPractice.Logic.Money;

public class SnackMachine : Entity
{
    public Money MoneyInside { get; private set; }
    public Money MoneyInTransaction { get; private set; }
    public virtual IList<Slot> Slots { get; private set; }

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

    public void InsertMoney(Money money)
    {
        Money[] notes = { TenRub, FiftyRub, HundredRub, FiveHundredRub, ThousandRub, FiveThousandRub };
        if (!notes.Contains(money))
            throw new InvalidOperationException();

        MoneyInTransaction += money;
    }

    public void ReturnMoney()
    {
        MoneyInTransaction = None;
    }

    public void BuySnack(int position = 0)
    {
        Slot slot = Slots.Single(x=>x.Position == position);
        slot.Quantity--;

        MoneyInside += MoneyInTransaction;
        MoneyInTransaction = None;
    }

    public void LoadSnacks(int position, Snack snack, int quantity, int price)
    {
        Slot slot = Slots.Single(x => x.Position == position);
        slot.Snack = snack;
        slot.Quantity = quantity;
        slot.Price = price;
    }
}
