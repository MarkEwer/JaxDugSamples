using System.Collections.Generic;
using System.Linq;

namespace Convert_Algorithm_to_Strategy.ObjectVersion
{
    public class TotalDonationsEngagementStrategy : IEngagementStrategy
    {
        public decimal CalculateCustomerEngagementForDay(IEnumerable<Sale> sales)
        {
            return sales
                .Where(x => x.Donations > 0)
                .Sum(x => x.Donations);
        }
    }
}
