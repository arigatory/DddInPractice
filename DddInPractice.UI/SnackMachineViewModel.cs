using DddInPractice.Logic;
using DddInPractice.UI.Common;
using System;

namespace DddInPractice.UI;

public class SnackMachineViewModel : ViewModel
{
    private readonly SnackMashine _snackMashine;

    public override string Caption => "Snack Machine";
    public string MoneyInTransaction => _snackMashine.MoneyInTransaction.ToString();
    public Money MoneyInside => _snackMashine.MoneyInside + _snackMashine.MoneyInTransaction;

    private string _message = "";
    public string Message
    {
        get { return _message; }
        private set
        {
            _message = value;
            Notify();
        }
    }

    public Command Insert10RubCommand { get; private set; }
    public Command Insert50RubCommand { get; private set; }
    public Command Insert100RubCommand { get; private set; }
    public Command Insert500RubCommand { get; private set; }
    public Command Insert1000RubCommand { get; private set; }
    public Command Insert5000RubCommand { get; private set; }
    public Command ReturnMoneyCommand { get; private set; }
    public Command BuySnackCommand { get; private set; }

    public SnackMachineViewModel(SnackMashine snackMashine)
    {
        _snackMashine = snackMashine;

        Insert10RubCommand = new Command(()=>InsertMoney(Money.TenRub));
        Insert50RubCommand = new Command(()=>InsertMoney(Money.FiftyRub));
        Insert100RubCommand = new Command(()=>InsertMoney(Money.HundredRub));
        Insert500RubCommand = new Command(()=>InsertMoney(Money.FiveHundredRub));
        Insert1000RubCommand = new Command(()=>InsertMoney(Money.ThousandRub));
        Insert5000RubCommand = new Command(()=>InsertMoney(Money.FiveThousandRub));
        ReturnMoneyCommand = new Command(()=>ReturnMoney());
        BuySnackCommand = new Command(()=> BuySnack());

    }

    private void BuySnack()
    {
        _snackMashine.BuySnack();
        NotifyClient("Вы купили товар");

    }

    private void ReturnMoney()
    {
        _snackMashine.ReturnMoney();
        NotifyClient("Внесенная сумма была полностью возвращена");
    }

    private void InsertMoney(Money note)
    {
        _snackMashine.InsertMoney(note);
        NotifyClient("Вы внесли: " + note);
    }

    private void NotifyClient(string message)
    {
        Message = message;
        Notify(nameof(MoneyInTransaction));
        Notify(nameof(MoneyInside));
    }
}
