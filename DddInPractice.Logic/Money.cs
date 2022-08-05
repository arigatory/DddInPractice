namespace DddInPractice.Logic;

public sealed class Money : ValueObject<Money>
{
    public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
    public static readonly Money TenRub = new Money(1, 0, 0, 0, 0, 0);
    public static readonly Money FiftyRub = new Money(0, 1, 0, 0, 0, 0);
    public static readonly Money HundredRub = new Money(0, 0, 1, 0, 0, 0);
    public static readonly Money FiveHundredRub = new Money(0, 0, 0, 1, 0, 0);
    public static readonly Money ThousandRub = new Money(0, 0, 0, 0, 1, 0);
    public static readonly Money FiveThousandRub = new Money(0, 0, 0, 0, 0, 1);



    public int TenRubCount { get; }
    public int FiftyRubCount { get; }
    public int HundredRubCount { get; }
    public int FiveHundredRubCount { get; }
    public int ThousandRubCount { get; }
    public int FiveThousandRubCount { get; }

    public int Amount => TenRubCount * 10 +
                FiftyRubCount * 50 +
                HundredRubCount * 100 +
                FiveHundredRubCount * 500 +
                ThousandRubCount * 1000 +
                FiveThousandRubCount * 5000;

    public Money(
        int tenRubCount,
        int fiftyRubCount,
        int hundredRubCount,
        int fiveHundredRubCount,
        int thousandRubCount,
        int fiveThousandRubCount)
    {
        if (tenRubCount < 0)
            throw new InvalidOperationException();
        if (fiftyRubCount < 0)
            throw new InvalidOperationException();
        if (hundredRubCount < 0)
            throw new InvalidOperationException();
        if (fiveHundredRubCount < 0)
            throw new InvalidOperationException();
        if (thousandRubCount < 0)
            throw new InvalidOperationException();
        if (fiveThousandRubCount < 0)
            throw new InvalidOperationException();

        TenRubCount = tenRubCount;
        FiftyRubCount = fiftyRubCount;
        HundredRubCount = hundredRubCount;
        FiveHundredRubCount = fiveHundredRubCount;
        ThousandRubCount = thousandRubCount;
        FiveThousandRubCount = fiveThousandRubCount;
    }

    public static Money operator +(Money money1, Money money2)
    {
        Money sum = new Money(
            money1.TenRubCount + money2.TenRubCount,
            money1.FiftyRubCount + money2.FiftyRubCount,
            money1.HundredRubCount + money2.HundredRubCount,
            money1.FiveHundredRubCount + money2.FiveHundredRubCount,
            money1.ThousandRubCount + money2.ThousandRubCount,
            money1.FiveThousandRubCount + money2.FiveThousandRubCount);

        return sum;
    }

    public static Money operator -(Money money1, Money money2)
    {
        Money sum = new Money(
            money1.TenRubCount - money2.TenRubCount,
            money1.FiftyRubCount - money2.FiftyRubCount,
            money1.HundredRubCount - money2.HundredRubCount,
            money1.FiveHundredRubCount - money2.FiveHundredRubCount,
            money1.ThousandRubCount - money2.ThousandRubCount,
            money1.FiveThousandRubCount - money2.FiveThousandRubCount);

        return sum;
    }

    protected override bool EqualsCore(Money other)
    {
        return TenRubCount == other.TenRubCount
            && FiftyRubCount == other.FiftyRubCount
            && HundredRubCount == other.HundredRubCount
            && FiveHundredRubCount == other.FiveHundredRubCount
            && ThousandRubCount == other.ThousandRubCount
            && FiveThousandRubCount == other.FiveThousandRubCount;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = TenRubCount;
            hashCode = (hashCode * 397) ^ FiftyRubCount;
            hashCode = (hashCode * 397) ^ HundredRubCount;
            hashCode = (hashCode * 397) ^ FiveHundredRubCount;
            hashCode = (hashCode * 397) ^ ThousandRubCount;
            hashCode = (hashCode * 397) ^ FiveThousandRubCount;
            return hashCode;
        }
    }
}
