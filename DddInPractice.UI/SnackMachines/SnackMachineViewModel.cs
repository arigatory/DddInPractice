using DddInPractice.Logic.SharedKernel;
using DddInPractice.Logic.SnackMachines;
using DddInPractice.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DddInPractice.UI.SnackMachines;

public class SnackMachineViewModel : ViewModel
{
    private readonly SnackMachine _snackMashine;
    private readonly SnackMachineRepository _repository;

    public override string Caption => "Snack Machine";
    public string MoneyInTransaction => _snackMashine.MoneyInTransaction.ToString();
    public Money MoneyInside => _snackMashine.MoneyInside;

    public IReadOnlyList<SnackPileViewModel> Piles
    {
        get
        {
            return _snackMashine.GetAllSnackPiles()
                .Select(x => new SnackPileViewModel(x))
                .ToList();
        }
    }

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
    public Command<string> BuySnackCommand { get; private set; }

    public SnackMachineViewModel(SnackMachine snackMashine)
    {
        _snackMashine = snackMashine;
        _repository = new SnackMachineRepository();

        Insert10RubCommand = new Command(() => InsertMoney(Money.TenRub));
        Insert50RubCommand = new Command(() => InsertMoney(Money.FiftyRub));
        Insert100RubCommand = new Command(() => InsertMoney(Money.HundredRub));
        Insert500RubCommand = new Command(() => InsertMoney(Money.FiveHundredRub));
        Insert1000RubCommand = new Command(() => InsertMoney(Money.ThousandRub));
        Insert5000RubCommand = new Command(() => InsertMoney(Money.FiveThousandRub));
        ReturnMoneyCommand = new Command(() => ReturnMoney());
        BuySnackCommand = new Command<string>(BuySnack);

    }

    private void BuySnack(string positonString)
    {
        int positon = int.Parse(positonString);

        string error = _snackMashine.CanBuySnack(positon);

        if (error != string.Empty)
        {
            NotifyClient(error);
            return;
        }

        _snackMashine.BuySnack(positon);
        _repository.Save(_snackMashine);
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
        Notify(nameof(Piles));
    }
}
