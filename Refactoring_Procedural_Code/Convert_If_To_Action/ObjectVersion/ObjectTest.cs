using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Convert_If_To_Action.ObjectVersion
{
    public class ObjectTest
    {
        [Fact]
        public void TestRestaurantNotYetOpen()
        {
            var c = new Clock(DateTime.Now.Date);
            var sut = new Restaurant(c);

            sut.ProcessDailySales(1000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(1));
            sut.ProcessDailySales(2000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(2));
            sut.ProcessDailySales(492.00m);

            Assert.Equal(3492m, sut.GrossSales);
            Assert.Equal(0m, sut.FranchiseeSales);
            Assert.Equal(3492m, sut.RoyaltiesDue);
        }

        [Fact]
        public void TestRestaurantInProbation()
        {
            var c = new Clock(DateTime.Now.Date);
            var sut = new Restaurant(c);

            sut.OpenRestaurant();

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(1));
            sut.ProcessDailySales(1000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(2));
            sut.ProcessDailySales(2000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(3));
            sut.ProcessDailySales(492.00m);

            Assert.Equal(3492m, sut.GrossSales);
            Assert.Equal(3492m, sut.FranchiseeSales);
            Assert.Equal(0m, sut.RoyaltiesDue);
        }

        [Fact]
        public void TestRestaurantLeavesProbation()
        {
            var c = new Clock(DateTime.Now.Date);
            var sut = new Restaurant(c);

            sut.OpenRestaurant();

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(1));
            sut.ProcessDailySales(1000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(8));
            sut.ProcessDailySales(0m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(9));
            sut.ProcessDailySales(2000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(10));
            sut.ProcessDailySales(492.00m);

            Assert.Equal(3492m, sut.GrossSales);
            Assert.Equal(3467.08m, sut.FranchiseeSales);
            Assert.Equal(24.92m, sut.RoyaltiesDue);
        }

        [Fact]
        public void TestRestaurantThatIsOpen()
        {
            var c = new Clock(DateTime.Now.Date);
            var sut = new Restaurant(c);

            sut.OpenRestaurant();

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(10));
            sut.ProcessDailySales(0m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(11));
            sut.ProcessDailySales(1000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(12));
            sut.ProcessDailySales(2000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(13));
            sut.ProcessDailySales(492.00m);

            Assert.Equal(3492m, sut.GrossSales);
            Assert.Equal(3457.08m, sut.FranchiseeSales);
            Assert.Equal(34.92m, sut.RoyaltiesDue);
        }

        [Fact]
        public void TestRestaurantThatHasBeenTerminated()
        {
            var c = new Clock(DateTime.Now.Date);
            var sut = new Restaurant(c);

            sut.OpenRestaurant();

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(10));
            sut.ProcessDailySales(0);

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(11));
            sut.TerminateRestaurant();

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(12));
            sut.ProcessDailySales(1000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(13));
            sut.ProcessDailySales(2000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(13));
            sut.ProcessDailySales(492.00m);

            Assert.Equal(3492m, sut.GrossSales);
            Assert.Equal(3492m, sut.FranchiseeSales);
            Assert.Equal(0m, sut.RoyaltiesDue);
        }

        [Fact]
        public void TestRestaurantClosesAfterTermination()
        {
            var c = new Clock(DateTime.Now.Date);
            var sut = new Restaurant(c);

            sut.OpenRestaurant();

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(10));
            sut.ProcessDailySales(0);

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(11));
            sut.TerminateRestaurant();
            
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(12));
            sut.ProcessDailySales(1000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(14));
            sut.ProcessDailySales(1000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(16));
            sut.ProcessDailySales(1000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(18));
            sut.ProcessDailySales(1000.00m);

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(20));
            sut.ProcessDailySales(1000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(22));
            sut.ProcessDailySales(1000.00m);

            Assert.Equal(6000m, sut.GrossSales);
            Assert.Equal(4000m, sut.FranchiseeSales);
            Assert.Equal(2000m, sut.RoyaltiesDue);
        }

        [Fact]
        public void TestRestaurantThatIsClosed()
        {
            var c = new Clock(DateTime.Now.Date);
            var sut = new Restaurant(c);

            sut.OpenRestaurant();

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(10));
            sut.ProcessDailySales(0);

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(11));
            sut.TerminateRestaurant();

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(20));
            sut.ProcessDailySales(0);

            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(30));
            sut.ProcessDailySales(1000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(31));
            sut.ProcessDailySales(2000.00m);
            c.SetCurrentDateTime(DateTime.Now.Date.AddDays(32));
            sut.ProcessDailySales(492.00m);

            Assert.Equal(3492m, sut.GrossSales);
            Assert.Equal(0m, sut.FranchiseeSales);
            Assert.Equal(3492m, sut.RoyaltiesDue);
        }
    }
}
