using FluentNHibernate.Mapping;

namespace DddInPractice.Logic.Management;

public class HeadOfficeMap : ClassMap<HeadOffice>
{
    public HeadOfficeMap()
    {
        Id(x => x.Id);

        Map(x=>x.Balance);

        Component(x=> x.Cash, y=>
        {
            y.Map(x=> x.TenRubCount);
            y.Map(x=> x.FiftyRubCount);
            y.Map(x=> x.HundredRubCount);
            y.Map(x=> x.FiveHundredRubCount);
            y.Map(x=> x.ThousandRubCount);
            y.Map(x=> x.FiveThousandRubCount);
        });
    }
}
