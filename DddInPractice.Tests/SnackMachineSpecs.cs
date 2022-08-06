namespace DddInPractice.Tests;
using static DddInPractice.Logic.Money;

public class SnackMachineSpecs
{
    [Fact]
    public void Return_money_empties_money_in_transaction()
    {
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(FiveHundredRub);

        snackMachine.ReturnMoney();

        snackMachine.MoneyInTransaction.Amount.Should().Be(0);

    }

    [Fact]
    public void Inserted_money_goes_to_money_in_transaction()
    {
        var snackMachine = new SnackMachine();
        snackMachine.InsertMoney(TenRub);
        snackMachine.InsertMoney(ThousandRub);

        snackMachine.MoneyInTransaction.Amount.Should().Be(1010);
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
        snackMachine.LoadSnacks(1, new Snack("Some snack"), 10, 100);
        snackMachine.InsertMoney(HundredRub);

        snackMachine.BuySnack(1);


        snackMachine.MoneyInTransaction.Should().Be(None);
        snackMachine.MoneyInside.Amount.Should().Be(100);
        snackMachine.Slots.Single(x=>x.Position == 1).Quantity.Should().Be(9);
        }
}
