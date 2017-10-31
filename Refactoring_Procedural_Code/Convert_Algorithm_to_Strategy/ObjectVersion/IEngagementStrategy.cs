using System.Collections.Generic;

namespace Convert_Algorithm_to_Strategy.ObjectVersion
{
    public interface IEngagementStrategy
    {
        decimal CalculateCustomerEngagementForDay(IEnumerable<Sale> sales);
    }
}
