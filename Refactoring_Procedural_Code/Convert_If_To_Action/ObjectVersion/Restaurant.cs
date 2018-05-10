using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_If_To_Action.ObjectVersion
{
    public class Restaurant
    {
        public Restaurant(Clock clock)
        {
            this._clock = clock;
            this.ProcessDailySales=Closed;
        }

        #region Times

        public static int ProbationPeriod { get; } = 7;

        // Clock is used to calculate the date and time
        private Clock _clock;

        // Date the restaurant first processed any sales
        private DateTime OpeningDate { get; set; }

        // Date of termination notice.
        private DateTime TerminationDate { get; set; }

        #endregion Times

        #region Sales

        // Percentage of royalties the restaurant must pay to the Franchise
        public static decimal RoyaltiesRate { get; } = 0.01m;

        // total of all sales
        public decimal GrossSales { get; private set; }

        // Portion of sales for the Franchisee
        public decimal FranchiseeSales { get; private set; }

        // Portion of sales for the Franchise
        public decimal RoyaltiesDue { get; private set; }

        #endregion Sales

        #region State Changes

        public void OpenRestaurant()
        {
            this.OpeningDate = this._clock.GetCurrentDate();
            this.ProcessDailySales = Probation;
        }

        public void TerminateRestaurant()
        {
            this.TerminationDate = this._clock.GetCurrentDate();
            this.ProcessDailySales = Terminated;
        }

        #endregion State Changes

        #region Actions

        public Action<decimal> ProcessDailySales { get; set; }

        private void Closed(decimal amount)
        {
            this.GrossSales += amount;
            this.RoyaltiesDue += amount;
        }

        private void Probation(decimal amount)
        {
            this.GrossSales += amount;
            this.FranchiseeSales += amount;
            if (this._clock.GetCurrentDate() >= 
                this.OpeningDate.AddDays(ProbationPeriod))
            {
                this.ProcessDailySales = Open;
            }
        }

        private void Open(decimal amount)
        {
            this.GrossSales += amount;
            this.FranchiseeSales += amount - (amount * RoyaltiesRate);
            this.RoyaltiesDue += amount * RoyaltiesRate;
        }

        private void Terminated(decimal amount)
        {
            this.GrossSales += amount;
            this.FranchiseeSales += amount;
            if (this._clock.GetCurrentDate() >= 
                this.TerminationDate.AddDays(ProbationPeriod))
            {
                this.ProcessDailySales = Closed;
            }
        }

        #endregion Actions
    }
}
