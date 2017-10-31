using System.Collections.Generic;
using System.Linq;

namespace Convert_Algorithm_to_Strategy.ProceduralVersion
{
    public class Register
    {
        public string Name { get; private set; }
        private IEnumerable<Sale> _sales;
        public Register(string name, IEnumerable<Sale> dailySales)
        {
            Name = name;
            _sales = dailySales;
        }

        private decimal _customerEngagement = 0m;
        public decimal CustomerEngagement
        {
            get
            {
                if(_customerEngagement==0m)
                {
                    this.CalculateCustomerEngagementForDay();
                }
                return _customerEngagement;
            }
        }
        private void CalculateCustomerEngagementForDay()
        {
            _customerEngagement = this._sales
                .Where(x => x.Donations > 0)
                .Sum(x => x.Donations);
        }
    }
}
