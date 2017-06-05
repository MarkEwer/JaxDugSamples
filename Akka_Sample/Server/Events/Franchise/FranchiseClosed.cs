using System;

namespace Server.Events.Franchise
{
    public class FranchiseClosed
    {
        public FranchiseClosed(string franchiseId, DateTime closingDate)
        {
            this.FranchiseId = franchiseId;
            this.ClosedOn = closingDate;
        }
        public string FranchiseId { get; }
        public DateTime ClosedOn { get; }
    }
}
