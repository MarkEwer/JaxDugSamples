using System;

namespace Server.Commands.Franchise
{
    public class RefuseFranchiseAgreement
    {
        public RefuseFranchiseAgreement(string franchiseId, DateTime refusalDate)
        {
            this.FranchiseId = franchiseId;
            this.RefusedOn = refusalDate;
        }
        public string FranchiseId { get; }
        public DateTime RefusedOn { get; }
    }
}
