namespace DddInPractice.Logic.SnackMachines;

public class SnackMachineDto
{
    public long Id { get; private set; }
    public int MoneyInside { get; private set; }

    public SnackMachineDto(long id, int moneyInside)
    {
        Id = id;
        MoneyInside = moneyInside;
    }
}
