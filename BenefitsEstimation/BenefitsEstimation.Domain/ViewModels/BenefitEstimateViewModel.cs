using Benefits.Domain.Models;
using d60.Cirqus.Views.ViewManagers;
using d60.Cirqus.Views.ViewManagers.Locators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benefits.Domain.Events;

namespace Benefits.Domain.ViewModels
{
    public class BenefitEstimateViewModel : IViewInstance<InstancePerAggregateRootLocator>
        , ISubscribeTo<Events.EstimateCreated>
        , ISubscribeTo<Events.DependentAdded>
        , ISubscribeTo<Events.DependentRemoved>
        , ISubscribeTo<Events.SalarySpecified>
        , ISubscribeTo<Events.SpouseAdded>
        , ISubscribeTo<Events.SpouseRemoved>
    {
        public string Id { get; set; }
        public long LastGlobalSequenceNumber { get; set; }


        #region Model Data
        public Person? Employee { get; protected set; }
        public Person? Spouse { get; protected set; }
        public MaritalStatus MaritalStatus { get; protected set; }
        public decimal Salary { get; protected set; }
        public bool InludeSpouse { get; protected set; }
        public int NumberOfDependantChildren { get; protected set; }
        public int NumberOfPaychecksPerYear { get; protected set; }
        public decimal DeductionPerPaycheck { get; protected set; }
        public decimal AnnualBenefitsCost { get; protected set; }
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
        public void Handle(IViewContext context, Events.EstimateCreated domainEvent)
        {
            this.Employee = new Person(domainEvent.FirstName, domainEvent.LastName, Config.BaseAnnualEmployeeBenefitCost);
            this._dependents = new List<Person>();
            this.MaritalStatus = MaritalStatus.Single;
            this.CalculateEstimate();
        }

        public void Handle(IViewContext context, Events.SalarySpecified domainEvent)
        {
            this.Salary = domainEvent.AnnualSalary;
            this.NumberOfPaychecksPerYear = domainEvent.NumberOfPaychecksPerYear;
            this.CalculateEstimate();
        }

        public void Handle(IViewContext context, Events.SpouseAdded domainEvent)
        {
            this.Spouse = new Person(domainEvent.FirstName, domainEvent.LastName, Config.BaseAnnualDependentBenefitCost);
            this.InludeSpouse = true;
            this.MaritalStatus = MaritalStatus.Maried;
            this.CalculateEstimate();
        }

        public void Handle(IViewContext context, Events.DependentAdded domainEvent)
        {
            this._dependents.Add(new Person(domainEvent.FirstName, domainEvent.LastName, Config.BaseAnnualDependentBenefitCost));
            this.CalculateEstimate();
        }

        public void Handle(IViewContext context, Events.SpouseRemoved domainEvent)
        {
            this.Spouse = null;
            this.InludeSpouse = false;
            this.MaritalStatus = MaritalStatus.Single;
            this.CalculateEstimate();
        }

        public void Handle(IViewContext context, Events.DependentRemoved domainEvent)
        {
            this._dependents.RemoveAll(x =>
                   x.FirstName.Equals(domainEvent.FirstName, StringComparison.OrdinalIgnoreCase)
                && x.LastName.Equals(domainEvent.LastName, StringComparison.OrdinalIgnoreCase));
            this.CalculateEstimate();
        }
        #endregion Event Handlers

        private void CalculateEstimate()
        {
            var cost = 0m;

            if (this.Employee.HasValue) cost += this.Employee.Value.ApplyDiscountRate(Config.BaseAnnualEmployeeBenefitCost);
            if (this.Spouse.HasValue) cost += this.Spouse.Value.ApplyDiscountRate(Config.BaseAnnualDependentBenefitCost);
            foreach(var d in _dependents) cost += d.ApplyDiscountRate(Config.BaseAnnualDependentBenefitCost);

            this.AnnualBenefitsCost = cost;
            if (this.NumberOfPaychecksPerYear == 0)
                this.DeductionPerPaycheck = 0;
            else
                this.DeductionPerPaycheck = Math.Round(cost / this.NumberOfPaychecksPerYear, 2);
        }
    }
}
