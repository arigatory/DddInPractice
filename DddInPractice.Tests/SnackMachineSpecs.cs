﻿namespace DddInPractice.Tests;
using static DddInPractice.Logic.Money;

public class SnackMachineSpecs
{
    [Fact]
    public void Return_money_empties_money_in_transaction()
    {
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(FiveHundredRub);

        snackMachine.ReturnMoney();

        snackMachine.MoneyInTransaction.Should().Be(0);

    }

    [Fact]
    public void Inserted_money_goes_to_money_in_transaction()
    {
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(TenRub);
        snackMachine.InsertMoney(ThousandRub);

        snackMachine.MoneyInTransaction.Should().Be(1010);
    }

    [Fact]
    public void Cannot_insert_more_than_one_note_at_a_time()
    {
        var snackMachine = new SnackMachine();
        var twentyRub = TenRub + TenRub;

        Action action = () => snackMachine.InsertMoney(twentyRub);

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void BuySnack_trades_inserted_money_for_a_snack()
    {
        var snackMachine = new SnackMachine();
        snackMachine.LoadSnacks(1, new SnackPile(new Snack("Some snack"), 10, 100));
        snackMachine.InsertMoney(HundredRub);

        snackMachine.BuySnack(1);


        snackMachine.MoneyInTransaction.Should().Be(0);
        snackMachine.MoneyInside.Amount.Should().Be(100);
        snackMachine.GetSnackPile(1).Quantity.Should().Be(9);
    }

    [Fact]
    public void Cannot_make_purchase_when_there_is_no_snacks()
    {
        var snackMachine = new SnackMachine();

        Action action = () => { snackMachine.BuySnack(1); };

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Cannot_make_purchase_if_not_enough_money_inserted()
    {
        var snackMachine = new SnackMachine();
        snackMachine.LoadSnacks(1, new SnackPile(new Snack("Some snack"), 1, 100));
        snackMachine.InsertMoney(TenRub);

        Action action = () => snackMachine.BuySnack(1);

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Snack_machine_returns_money_with_highest_denominatiuon_first()
    {
        SnackMachine snackMachine = new SnackMachine();
        snackMachine.LoadMoney(FiveHundredRub);

        snackMachine.InsertMoney(HundredRub);
        snackMachine.InsertMoney(HundredRub);
        snackMachine.InsertMoney(HundredRub);
        snackMachine.InsertMoney(HundredRub);
        snackMachine.InsertMoney(HundredRub);
        snackMachine.ReturnMoney();

        snackMachine.MoneyInside.FiveHundredRubCount.Should().Be(0);
        snackMachine.MoneyInside.HundredRubCount.Should().Be(5);
    }
}
