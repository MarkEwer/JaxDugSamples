using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain.Models
{
    public static class Config
    {
        public static readonly decimal BaseAnnualEmployeeBenefitCost = 1000m;
        public static readonly decimal BaseAnnualDependentBenefitCost = 500m;
        public static readonly decimal DefaultDiscountRate = 1m;
        public static readonly decimal DiscountRateForACustomers = 0.9m;
    }
}
