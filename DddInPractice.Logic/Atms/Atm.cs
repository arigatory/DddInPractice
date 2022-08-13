using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;
using static DddInPractice.Logic.SharedKernel.Money;

namespace DddInPractice.Logic.Atms;

public class Atm : AggregateRoot
{
    private const double CommissionRate = 0.01;
    public virtual Money MoneyInside { get; set; } = None;
    public virtual int MoneyCharged { get; set; }

    public virtual string CanTakeMoney(int amount)
    {
        if (amount < 10)
            return "Нельзя снимать сумму меньше 10 рублей.";

        if (MoneyInside.Amount < amount)
            return $"В банкомате недостаточно средств: {MoneyInside.Amount} руб.";

        if (!MoneyInside.CanAllocate(amount))
            return $"Нет возможности разменять средства для выдачи денег";

        return string.Empty;
    }

    public virtual void TakeMoney(int amount)
    {
        if (CanTakeMoney(amount) != string.Empty)
        {
            throw new InvalidOperationException();
        }
        Money output = MoneyInside.Allocate(amount);
        MoneyInside -= output;

        int amountWithCommission = CalculateAmountWithComission(amount);
        MoneyCharged += amountWithCommission;

        AddDomainEvent(new BalanceChangedEvent(amountWithCommission));
    }

    public virtual int CalculateAmountWithComission(int amount)
    {
        int commission = (int)Math.Ceiling(amount * CommissionRate);
        int lessThen10Rub = commission % 10;
        if (lessThen10Rub > 0)
        {
            commission = commission - lessThen10Rub + 10;
        }
        return amount + commission;
    }

    public virtual void LoadMoney(Money money)
    {
        MoneyInside += money;
    }
}
