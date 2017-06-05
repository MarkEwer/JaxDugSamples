using Server.ValueObjects;
using System;

namespace Server.Events.Franchise
{
    public class FranchiseSold
    {
        public FranchiseSold(string franchiseId, DateTime soldOnDate, string newCorporateName, string newOwnerGiveName, 
            string newOwnerSurname)
        {
            this.FranchiseId = franchiseId;
            this.SoldOnDate = soldOnDate;
            this.NewCorporateName = newCorporateName;
            this.NewOwnerGiveName = newOwnerGiveName;
            this.NewOwnerSurname = newOwnerSurname;
        }
        public string FranchiseId { get; }
        public DateTime SoldOnDate { get; }
        public string NewCorporateName { get; }
        public string NewOwnerGiveName { get; }
        public string NewOwnerSurname { get; }
    }
}
