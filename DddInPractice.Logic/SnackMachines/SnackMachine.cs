﻿using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;

namespace DddInPractice.Logic.SnackMachines;

using static DddInPractice.Logic.SharedKernel.Money;

public class SnackMachine : AggregateRoot
{
    public virtual Money MoneyInside { get; protected set; }
    public virtual int MoneyInTransaction { get; protected set; }
    protected virtual IList<Slot> Slots { get; set; }

    public SnackMachine()
    {
        MoneyInside = None;
        MoneyInTransaction = 0;
        Slots = new List<Slot>
        {
            new Slot(this, 1),
            new Slot(this, 2),
            new Slot(this, 3)
        };
    }

    public virtual SnackPile GetSnackPile(int position)
    {
        return GetSlot(position).SnackPile;
    }

    public virtual IReadOnlyList<SnackPile> GetAllSnackPiles()
    {
        return Slots.OrderBy(x => x.Position)
            .Select(x => x.SnackPile)
            .ToList();
    }

    private Slot GetSlot(int position)
    {
        return Slots.Single(x => x.Position == position);

    }

    public virtual void InsertMoney(Money money)
    {
        Money[] notes = { TenRub, FiftyRub, HundredRub, FiveHundredRub, ThousandRub, FiveThousandRub };
        if (!notes.Contains(money))
            throw new InvalidOperationException();

        MoneyInTransaction += money.Amount;
        MoneyInside += money;
    }

    public virtual void ReturnMoney()
    {
        Money moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
        MoneyInside -= moneyToReturn;
        MoneyInTransaction = 0;
    }

    public virtual string CanBuySnack(int position)
    {
        SnackPile snackPile = GetSnackPile(position);

        if (snackPile.Quantity == 0)
            return "Данный товар закончился";

        if (MoneyInTransaction < snackPile.Price)
            return "Недостаточно денег";

        if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
            return "Нет сдачи";

        return string.Empty;
    }

    public virtual void BuySnack(int position)
    {
        if (CanBuySnack(position) != string.Empty)
            throw new InvalidOperationException();


        Slot slot = GetSlot(position);
        slot.SnackPile = slot.SnackPile.SubtractOne();

        Money change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
        MoneyInside -= change;
        MoneyInTransaction = 0;
    }

    public virtual void LoadSnacks(int position, SnackPile snackPile)
    {
        Slot slot = GetSlot(position);
        slot.SnackPile = snackPile;
    }

    public virtual void LoadMoney(Money money)
    {
        MoneyInside += money;
    }
}
