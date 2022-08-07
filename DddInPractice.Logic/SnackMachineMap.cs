using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace DddInPractice.Logic;

public class SnackMachineMap : ClassMap<SnackMachine>
{
    public SnackMachineMap()
    {
        Id(x => x.Id);

        Component(x => x.MoneyInside, y =>
        {
            y.Map(x => x.TenRubCount);
            y.Map(x => x.FiftyRubCount);
            y.Map(x => x.HundredRubCount);
            y.Map(x => x.FiveHundredRubCount);
            y.Map(x => x.ThousandRubCount);
            y.Map(x => x.FiveThousandRubCount);
        });

        HasMany<Slot>(Reveal.Member<SnackMachine>("Slots")).Cascade.SaveUpdate().Not.LazyLoad();
    }
}
