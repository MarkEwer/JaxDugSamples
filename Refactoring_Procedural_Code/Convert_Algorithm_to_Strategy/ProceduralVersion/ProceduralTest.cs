using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Convert_Algorithm_to_Strategy.ProceduralVersion
{
    public class ProceduralTest
    {
        [Fact]
        public void TestEngagementContestWinner()
        {
            var registers = new List<Register>
            {
                new Register("Front", Transactions.FrontRegisterSales),
                new Register("Back", Transactions.BackRegisterSales),
                new Register("Drive Thru", Transactions.DriveThruSales)
            };

            var sut = new Contest(registers);
            var result = sut.GetWinner();

            Assert.Equal("Front", result);
        }
    }
}
