using DddInPractice.Logic.Atms;
using DddInPractice.Logic.Common;
using DddInPractice.Logic.Utils;
using static DddInPractice.Logic.SharedKernel.Money;

namespace DddInPractice.Tests;

public class AtmSpecs
{
    [Fact]
    public void Take_money_exchange_money_with_commision()
    {
        var atm = new Atm();
        atm.LoadMoney(HundredRub);

        atm.TakeMoney(100);

        atm.MoneyInside.Amount.Should().Be(0);
        // комиссия 1%
        atm.MoneyCharged.Should().Be(110);
    }

    [Fact]
    public void Commission_is_at_least_ten_rub()
    {
        var atm = new Atm();
        atm.LoadMoney(TenRub);

        atm.TakeMoney(10);

        atm.MoneyCharged.Should().Be(20);
    }

    [Fact]
    public void Commission_is_rounded_up_to_the_next_10_rub()
    {
        var atm = new Atm();
        atm.LoadMoney(HundredRub + TenRub);

        atm.TakeMoney(110);

        atm.MoneyCharged.Should().Be(120);
    }

    [Fact]
    public void Take_money_raises_an_event()
    {
        Atm atm = new Atm();
        atm.LoadMoney(HundredRub);
        
        atm.TakeMoney(100);

        atm.ShouldContainBalanceChangedEvent(110);
    }
}

internal static class AtmExtensions
{
    public static void ShouldContainBalanceChangedEvent(this Atm atm, int delta)
    {
        BalanceChangedEvent domainEvent = (BalanceChangedEvent)atm.DomainEvents
            .SingleOrDefault(x=> x.GetType() == typeof(BalanceChangedEvent));

        domainEvent.Should().NotBeNull();
        domainEvent.Delta.Should().Be(delta);
    }
}