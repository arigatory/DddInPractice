using FluentNHibernate.Mapping;

namespace DddInPractice.Logic.Atms;

public class AtmMap : ClassMap<Atm>
{
    public AtmMap()
    {
        Id(x => x.Id);

        Map(x => x.MoneyCharged);

        Component(x => x.MoneyInside, y =>
        {
            y.Map(x => x.TenRubCount);
            y.Map(x => x.FiftyRubCount);
            y.Map(x => x.HundredRubCount);
            y.Map(x => x.FiveHundredRubCount);
            y.Map(x => x.ThousandRubCount);
            y.Map(x => x.FiveThousandRubCount);
        });
    }
}
