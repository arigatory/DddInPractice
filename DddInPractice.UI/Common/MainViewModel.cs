using DddInPractice.Logic;

namespace DddInPractice.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            var viewModel = new SnackMachineViewModel(new SnackMachine());
            _dialogService.ShowDialog(viewModel);
        }
    }
}
