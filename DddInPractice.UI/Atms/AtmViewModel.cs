using DddInPractice.Logic.Atm;
using DddInPractice.Logic.SharedKernel;
using DddInPractice.UI.Common;
using System;

namespace DddInPractice.UI.Atms
{
    public class AtmViewModel : ViewModel
    {
        private readonly Atm _atm;
        private readonly AtmRepository _repository;
        private readonly PaymentGateway _paymentGateway;
        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                Notify();
            }
        }

        public override string Caption => "ATM";
        public Money MoneyInside => _atm.MoneyInside;
        public string MoneyCharged => _atm.MoneyCharged.ToString("C0");
        public Command<int> TakeMoneyCommand { get; private set; }


        public AtmViewModel(Atm atm)
        {
            _atm = atm;
            _repository = new AtmRepository();
            _paymentGateway = new PaymentGateway();

            TakeMoneyCommand = new Command<int>(x => x > 0, TakeMoney);
        }

        private void TakeMoney(int amount)
        {
            string error = _atm.CanTakeMoney(amount);
            if (error != string.Empty)
            {
                NotifyClient(error);
                return;
            }
            int calculateAmountWithComission = _atm.CalculateAmountWithComission(amount);
            _paymentGateway.ChargePayment(calculateAmountWithComission);
            _atm.TakeMoney(amount);
            _repository.Save(_atm);

            NotifyClient("Вы сняли " + amount.ToString("C0"));
        }

        private void NotifyClient(string error)
        {
            Message = error;
            Notify(nameof(MoneyInside));
            Notify(nameof(MoneyCharged));
        }
    }
}
