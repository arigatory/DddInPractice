using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace DddInPractice.Tests
{
    public class TemporaryTests
    {
        [Fact]
        public void Test()
        {
            SessionFactory.Init(@"Server=(localdb)\mssqllocaldb;Database=DddInPractice;Trusted_Connection=true");

            using (ISession session = SessionFactory.OpenSession())
            {
                long id = 1;
                var snackMachine = session.Get<SnackMachine>(id);
            }
        }
    }
}
