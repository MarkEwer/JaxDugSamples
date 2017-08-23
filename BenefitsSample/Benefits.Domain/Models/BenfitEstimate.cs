using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain.Models
{
    public class BenfitEstimate
    {
        private List<Person> _dependents;

        public BenfitEstimate(string id)
        {
            this.Id = id;
            this._dependents = new List<Person>();
        }

        public string Id { get; protected set; }
        public Person? Employee { get; protected set; }
        public Person? Spouse { get; protected set; }
        public MaritalStatus MaritalStatus { get; protected set; }
        public decimal Salary { get; protected set; }
        public bool InludeSpouse { get; protected set; }

        public int NumberOfDependantChildren { get; protected set; }
        public int NumberOfPaychecksPerYear { get; protected set; }
        public decimal DeductionPerPaycheck { get; protected set; }
        public IEnumerable<Person> Dependents
        {
            get
            {
                return this._dependents.AsReadOnly().AsEnumerable();
            }
        }

        //Event Handlers
        public void Apply(Events.EstimateCreated evt)
        {
            this.Employee=new Person(evt.FirstName, evt.LastName);
            this.MaritalStatus = evt.MaritalStatus;
        }

        public void Apply(Events.SalarySpecified evt)
        {
            this.Salary = evt.AnnualSalary;
            this.NumberOfPaychecksPerYear = evt.NumberOfPaychecksPerYear;
        }

        public void Apply(Events.SpouseAdded evt)
        {
            this.Spouse = new Person(evt.FirstName, evt.LastName);
            this.InludeSpouse = true;
        }

        public void Apply(Events.DependentAdded evt)
        {
            this._dependents.Add(new Person(evt.FirstName, evt.LastName));
        }

        public void Apply(Events.SpouseRemoved evt)
        {
            this.Spouse = null;
        }

        public void Apply(Events.DependentRemoved evt)
        {
            this._dependents.RemoveAll(x =>
                   x.FirstName.Equals(evt.FirstName, StringComparison.OrdinalIgnoreCase)
                && x.LastName.Equals(evt.LastName, StringComparison.OrdinalIgnoreCase));
        }

        //Quote Generator
        public Estimate BroadcastEstimate()
        {
            try
            {
                var est = new Estimate();

                est = this.SetEmployeeInfo(est);
                est = this.SetSpouseInfo(est);
                est = this.SetDepedantsInfo(est);

                return est;
            }
            catch (InvalidOperationException ex)
            {
                //TODO: Add logging for exception messages
                return null;
            }
        }

        private Estimate SetEmployeeInfo(Estimate estimate)
        {
            if (!this.Employee.HasValue) throw new InvalidOperationException("Employee must not be null");
            return AddPersonToEstimate(estimate, this.Employee.Value, Config.BaseAnnualEmployeeBenefitCost);
        }

        private Estimate SetSpouseInfo(Estimate estimate)
        {
            if (!this.Spouse.HasValue) return estimate;
            return AddPersonToEstimate(estimate, this.Spouse.Value, Config.BaseAnnualDependentBenefitCost);
        }

        private Estimate SetDepedantsInfo(Estimate estimate)
        {
            foreach(var dependant in this.Dependents)
            {
                estimate = AddPersonToEstimate(estimate, dependant, Config.BaseAnnualDependentBenefitCost);
            }
            return estimate;
        }

        private static Estimate AddPersonToEstimate(Estimate estimate, Person person, decimal baseAnnualCost)
        {
            estimate.FirstName = person.FirstName;
            estimate.LastName = person.LastName;
            estimate.AnnualCost += person.ApplyDiscountRate(baseAnnualCost);
            return estimate;
        }
    }
    public class Estimate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualCost { get; set; }
    }
}
