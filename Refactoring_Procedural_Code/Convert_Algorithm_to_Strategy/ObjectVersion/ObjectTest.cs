using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Convert_Algorithm_to_Strategy.ObjectVersion
{
    public class ObjectTest
    {
        [Fact]
        public void TestEngagementContestWinnerWithTotalDonationStrategy()
        {
            var registers = new List<Register>
            {
                new Register("Front", Transactions.FrontRegisterSales, new TotalDonationsEngagementStrategy()),
                new Register("Back", Transactions.BackRegisterSales, new TotalDonationsEngagementStrategy()),
                new Register("Drive Thru", Transactions.DriveThruSales, new TotalDonationsEngagementStrategy())
            };

            var sut = new Contest(registers);
            var result = sut.GetWinner();

            Assert.Equal("Front", result);
        }

        [Fact]
        public void TestEngagementContestWinnerWithNumberOfDonorsStrategy()
        {
            var registers = new List<Register>
            {
                new Register("Front", Transactions.FrontRegisterSales, new NumberOfDonorsEngagementStrategy()),
                new Register("Back", Transactions.BackRegisterSales, new NumberOfDonorsEngagementStrategy()),
                new Register("Drive Thru", Transactions.DriveThruSales, new NumberOfDonorsEngagementStrategy())
            };

            var sut = new Contest(registers);
            var result = sut.GetWinner();

            Assert.Equal("Drive Thru", result);
        }
    }
}
