namespace DddInPractice.Logic;
using static DddInPractice.Logic.Money;

public sealed class SnackMashine : Entity
{
    public Money MoneyInside { get; private set; } = None;
    public Money MoneyInTransaction { get; private set; } = None;

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

    public void BuySnack()
    {
        MoneyInside += MoneyInTransaction;
        MoneyInTransaction = None;
    }
}
