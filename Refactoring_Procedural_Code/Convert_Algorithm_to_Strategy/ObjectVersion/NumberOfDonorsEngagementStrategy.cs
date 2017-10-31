using System.Collections.Generic;
using System.Linq;

namespace Convert_Algorithm_to_Strategy.ObjectVersion
{
    public class NumberOfDonorsEngagementStrategy : IEngagementStrategy
    {
        public decimal CalculateCustomerEngagementForDay(IEnumerable<Sale> sales)
        {
            return sales
                .Count(x => x.Donations > 0);
        }
    }
}
