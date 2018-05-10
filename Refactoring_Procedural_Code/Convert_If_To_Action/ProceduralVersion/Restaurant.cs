using System;
using System.Collections.Generic;
using System.Text;

namespace Convert_If_To_Action.ProceduralVersion
{
    public class Restaurant
    {
        public Restaurant(Clock clock)
        {
            this._clock = clock;
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

        #region Flags

        // Probationary restaurants are not required to pay royalties yet.
        // Probation lasts for 7 days after the opening date
        public bool IsOnProbation { get; set; }

        // Open restaurants have a valid franchisee license to operate.  If
        // a restaurant processes a sale while it is not open, then all sales
        // go to the Franchise
        public bool IsOpen { get; set; }

        // Terminated restaurants have had their license to operate revoked 
        // and must close within 7 days.  Restaurants do not pay royalties
        // during their termination period.
        public bool IsTerminated { get; set; }

        #endregion Flags

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

        public void OpenRestaurant()
        {
            this.OpeningDate = this._clock.GetCurrentDate();
            this.IsOpen = true;
            this.IsOnProbation = true;
        }

        public void TerminateRestaurant()
        {
            this.TerminationDate = this._clock.GetCurrentDate();
            this.IsTerminated = true;
        }

        public void ProcessDailySales(decimal amount)
        {
            if(!this.IsOpen)
            {
                this.GrossSales += amount;
                this.RoyaltiesDue += amount;
                return;
            }
            if (this.IsOpen && this.IsOnProbation)
            {
                this.GrossSales += amount;
                this.FranchiseeSales += amount;
                if(this._clock.GetCurrentDate() >= 
                    this.OpeningDate.AddDays(ProbationPeriod))
                {
                    this.IsOnProbation = false;
                }
                return;
            }
            if (this.IsOpen && this.IsTerminated)
            {
                this.GrossSales += amount;
                this.FranchiseeSales += amount;
                if (this._clock.GetCurrentDate() >= 
                    this.TerminationDate.AddDays(ProbationPeriod))
                {
                    this.IsOpen = false;
                }
                return;
            }
            if (this.IsOpen && !this.IsOnProbation && !this.IsTerminated)
            {
                this.GrossSales += amount;
                this.FranchiseeSales += amount - (amount * RoyaltiesRate);
                this.RoyaltiesDue += amount * RoyaltiesRate;
                return;
            }
        }
    }
}
