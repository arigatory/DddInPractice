using DddInPractice.UI.Management;

namespace DddInPractice.UI.Common;

public class MainViewModel : ViewModel
{
    public DashboardViewModel Dashboard { get; set; }
    public MainViewModel()
    {
        Dashboard = new DashboardViewModel();
    }
}
