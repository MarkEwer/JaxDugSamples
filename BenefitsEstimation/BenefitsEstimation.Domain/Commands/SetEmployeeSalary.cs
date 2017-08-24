using d60.Cirqus.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain.Commands
{
    public class SetEmployeeSalary: Command<BenefitEstimateRoot>
    {
        public SetEmployeeSalary(string id, decimal annualSalary, int numberOfPaychecksPerYear):base(id)
        {
            this.AnnualSalary = annualSalary;
            this.NumberOfPaychecksPerYear = numberOfPaychecksPerYear;
        }

        public decimal AnnualSalary { get; protected set; }
        public int NumberOfPaychecksPerYear { get; protected set; }

        public override void Execute(BenefitEstimateRoot aggregateRoot)
        {
            aggregateRoot.SetSalary(this);
        }
    }
}
