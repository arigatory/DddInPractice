using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;

namespace DddInPractice.Logic.Management;

public class HeadOffice : AggregateRoot
{
    public virtual int Balance { get; protected set; }
    public virtual Money Cash { get; protected set; } = Money.None;

    public virtual void ChangeBalance(int delta)
    {
        Balance += delta;
    }
}
