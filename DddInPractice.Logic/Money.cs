namespace DddInPractice.Logic;

public class Money
{
    public int TenRubCount { get; set; }
    public int FiftyRubCount { get; set; }
    public int HundredRubCount { get; set; }
    public int FiveHundredRubCount { get; set; }
    public int ThousandRubCount { get; set; }
    public int FiveThousandRubCount { get; set; }

    public Money(
        int tenRubCount,
        int fiftyRubCount,
        int hundredRubCount,
        int fiveHundredRubCount,
        int thousandRubCount,
        int fiveThousandRubCount)
    {
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
}
