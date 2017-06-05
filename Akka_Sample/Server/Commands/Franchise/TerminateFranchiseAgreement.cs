using System;

namespace Server.Commands.Franchise
{
    public class TerminateFranchiseAgreement
    {
        public TerminateFranchiseAgreement(string franchiseId, DateTime terminationDate)
        {
            this.FranchiseId = franchiseId;
            this.TerminationDate = terminationDate;
        }
        public string FranchiseId { get; }
        public DateTime TerminationDate { get; }
    }
}
