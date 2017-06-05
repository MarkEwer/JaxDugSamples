using System;

namespace Server.Events.Franchise
{
    public class FranchiseAgreementSigned
    {
        public FranchiseAgreementSigned(string franchiseId, DateTime signedOnDate)
        {
            this.FranchiseId = franchiseId;
            this.SignedOnDate = signedOnDate;
        }
        public string FranchiseId { get; }
        public DateTime SignedOnDate { get; }
    }
}
