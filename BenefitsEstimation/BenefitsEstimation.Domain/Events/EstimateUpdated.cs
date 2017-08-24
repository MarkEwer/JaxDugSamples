using Benefits.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain.Events
{
    public class EstimateUpdated:EventBase
    {
        public EstimateUpdated(string id, decimal annualCost, decimal costPerPaycheck):base(id)
        {
            this.AnnualCost = annualCost;
            this.CostPerPaycheck = costPerPaycheck;
        }
        public decimal AnnualCost { get; protected set; }
        public decimal CostPerPaycheck { get; protected set; }
    }
}
