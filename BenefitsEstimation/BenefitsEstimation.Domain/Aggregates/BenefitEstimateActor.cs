using Benefits.Domain.Events;
using Benefits.Domain.Models;
using d60.Cirqus.Aggregates;
using d60.Cirqus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain
{
    public class BenefitEstimateRoot : AggregateRoot
        , IEmit<Events.EstimateCreated>
        , IEmit<Events.DependentAdded>
        , IEmit<Events.DependentRemoved>
        , IEmit<Events.SalarySpecified>
        , IEmit<Events.SpouseAdded>
        , IEmit<Events.SpouseRemoved>

    {
        #region Model Data
        public Person? Employee { get; protected set; }
        public Person? Spouse { get; protected set; }
        public MaritalStatus MaritalStatus { get; protected set; }
        public decimal Salary { get; protected set; }
        public bool InludeSpouse { get; protected set; }
        public int NumberOfDependantChildren { get; protected set; }
        public int NumberOfPaychecksPerYear { get; protected set; }
        public decimal DeductionPerPaycheck { get; protected set; }
        private List<Person> _dependents;
        public IEnumerable<Person> Dependents
        {
            get
            {
                return this._dependents.AsReadOnly().AsEnumerable();
            }
        }

        #endregion Model Data

        #region Event Handlers
        public void Apply(Events.EstimateCreated e)
        {
            this.Employee = new Person(e.FirstName, e.LastName, Config.BaseAnnualEmployeeBenefitCost);
            this._dependents = new List<Person>();
            this.MaritalStatus = MaritalStatus.Single;
        }

        public void Apply(Events.SalarySpecified e)
        {
            this.Salary = e.AnnualSalary;
            this.NumberOfPaychecksPerYear = e.NumberOfPaychecksPerYear;
        }

        public void Apply(Events.SpouseAdded e)
        {
            this.Spouse = new Person(e.FirstName, e.LastName, Config.BaseAnnualDependentBenefitCost);
            this.MaritalStatus = MaritalStatus.Maried;
            this.InludeSpouse = true;
        }

        public void Apply(Events.DependentAdded e)
        {
            this._dependents.Add(new Person(e.FirstName, e.LastName, Config.BaseAnnualDependentBenefitCost));
        }
        
        public void Apply(Events.SpouseRemoved e)
        {
            this.Spouse = null;
            this.MaritalStatus = MaritalStatus.Single;
        }

        public void Apply(Events.DependentRemoved e)
        {
            this._dependents.RemoveAll(x =>
                   x.FirstName.Equals(e.FirstName, StringComparison.OrdinalIgnoreCase)
                && x.LastName.Equals(e.LastName, StringComparison.OrdinalIgnoreCase));
        }
        #endregion Event Handlers

        #region Command Handlers
        public void AddEmployee(Commands.AddEmployeeToBenefitsEstimate cmd)
        {
            if (this.Employee.HasValue)
            {
                throw new InvalidOperationException("Employee Information Already Exists");
            }
            else
            {
                var evt = new EstimateCreated(this.Id, cmd.FirstName, cmd.LastName);
                this.Emit(evt);
            }
        }

        public void SetSalary(Commands.SetEmployeeSalary cmd)
        {
            if (this.Employee.HasValue)
            {
                var evt = new Events.SalarySpecified(this.Id, cmd.AnnualSalary, cmd.NumberOfPaychecksPerYear);
                this.Emit(evt);
                //var estimate = this.BroadcastEstimate();
            }
            else
            {
                throw new InvalidOperationException("You must set an employee before you can assign a salary.");
            }
        }

        public void AddSpouse(Commands.AddSpouseToBenefitsEstimate cmd)
        {
            if(this.Spouse.HasValue)
            {
                throw new InvalidOperationException("You must set an employee before you can add a spouse.");
            }
            else
            {
                var evt = new Events.SpouseAdded(this.Id, cmd.FirstName, cmd.LastName);
                this.Emit(evt);
                //var estimate = this.BroadcastEstimate();
            }
        }

        public void AddDependent(Commands.AddDependentToBenefitsEstimate cmd)
        {
            if (this.Dependents.Any(x => x.FirstName.Equals(cmd.FirstName, StringComparison.OrdinalIgnoreCase)
                                            && x.LastName.Equals(cmd.LastName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Cannot add a duplicate dependent");
            }
            else
            {
                var evt = new Events.DependentAdded(this.Id, cmd.FirstName, cmd.LastName);
                this.Emit(evt);
                //var estimate = this.BroadcastEstimate();
            }
        }

        public void RemoveSpouse(Commands.RemoveSpouseToBenefitsEstimate cmd)
        {
            if(this.Spouse.HasValue
                && this.Spouse.Value.FirstName.Equals(cmd.FirstName, StringComparison.OrdinalIgnoreCase)
                && this.Spouse.Value.LastName.Equals(cmd.LastName, StringComparison.OrdinalIgnoreCase)
                )
            {
                var evt = new Events.SpouseRemoved(this.Id, cmd.FirstName, cmd.LastName);
                this.Emit(evt);
                //var estimate = this.BroadcastEstimate();
            }
            else
            {
                throw new InvalidOperationException("This person is not listed as the employee's spouse.");
            }
        }

        public void RemoveDependent(Commands.RemoveDependentToBenefitsEstimate cmd)
        {
            if (this.Dependents.Any(x => x.FirstName.Equals(cmd.FirstName, StringComparison.OrdinalIgnoreCase)
                                            && x.LastName.Equals(cmd.LastName, StringComparison.OrdinalIgnoreCase)))
            {
                var evt = new Events.DependentRemoved(this.Id, cmd.FirstName, cmd.LastName);
                this.Emit(evt);
                //var estimate = this.BroadcastEstimate();
            }
            else
            {
                throw new InvalidOperationException("This person is not listed as a dependent of the employee.");
            }
        }

        #endregion Command Handlers
    }
}
