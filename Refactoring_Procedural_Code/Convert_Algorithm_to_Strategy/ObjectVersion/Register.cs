using System.Collections.Generic;
using System.Linq;

namespace Convert_Algorithm_to_Strategy.ObjectVersion
{
    public class Register
    {
        public string Name { get; private set; }
        private IEnumerable<Sale> _sales;
        private IEngagementStrategy _strategy;
        public Register(string name, IEnumerable<Sale> dailySales,
            IEngagementStrategy strategy)
        {
            Name = name;
            _sales = dailySales;
            _strategy = strategy;
        }

        private decimal _customerEngagement = 0m;
        public decimal CustomerEngagement
        {
            get
            {
                if(_customerEngagement==0m)
                {
                    _customerEngagement = this._strategy.CalculateCustomerEngagementForDay(this._sales);
                }
                return _customerEngagement;
            }
        }

    }
}
