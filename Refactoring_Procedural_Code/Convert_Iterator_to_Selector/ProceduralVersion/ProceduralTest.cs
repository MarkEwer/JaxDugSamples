using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Convert_Iterator_to_Selector.ProceduralVersion
{
    public class ProceduralTest
    {
        [Fact]
        public void FindMostOrderedSandwich()
        {
            var sut = new Analysis();

            var found = sut.FindSandwichTypeOrderedMostOftenWithoutCoupon(Transactions.MondaySales);

            Assert.Equal("Firehouse Hero", found);
        }
    }
}
