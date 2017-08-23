using Benefits.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain.Commands
{
    public class SetEmployeeSalary:CommandBase
    {
        public SetEmployeeSalary(string id, decimal annualSalary, int numberOfPaychecksPerYear):base(id)
        {
            this.AnnualSalary = annualSalary;
            this.NumberOfPaychecksPerYear = numberOfPaychecksPerYear;
        }

        public decimal AnnualSalary { get; protected set; }
        public int NumberOfPaychecksPerYear { get; protected set; }
    }
}
