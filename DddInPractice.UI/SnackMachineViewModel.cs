using DddInPractice.Logic;
using DddInPractice.UI.Common;

namespace DddInPractice.UI;

public class SnackMachineViewModel : ViewModel
{
    private readonly SnackMashine _snackMashine;

    public override string Caption => "Snack Machine";

    public SnackMachineViewModel(SnackMashine snackMashine)
    {
        _snackMashine = snackMashine;
    }
}
