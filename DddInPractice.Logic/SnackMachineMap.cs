using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddInPractice.Logic
{
    public class SnackMachineMap : ClassMap<SnackMachine>
    {
        public SnackMachineMap()
        {
            Id(x=>x.Id);

            Component(x=>x.MoneyInside, y=>
            {
                y.Map(x=>x.TenRubCount);
                y.Map(x=>x.FiftyRubCount);
                y.Map(x=>x.HundredRubCount);
                y.Map(x=>x.FiveHundredRubCount);
                y.Map(x=>x.ThousandRubCount);
                y.Map(x=>x.FiveThousandRubCount);
            });
        }
    }
}
