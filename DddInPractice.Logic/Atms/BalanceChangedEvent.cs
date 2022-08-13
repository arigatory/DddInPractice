using DddInPractice.Logic.Common;

namespace DddInPractice.Logic.Atms;

public class BalanceChangedEvent : IDomainEvent
{
    public int Delta { get; private set; }
    public BalanceChangedEvent(int delta)
    {
        Delta = delta;
    }
}
