using Server.ValueObjects;
using System;

namespace Server.Commands.Franchise
{
    public class SellFranchise
    {
        public SellFranchise(string franchiseId, DateTime sellingDate, string newCorporateName, string newOwnerGiveName, 
            string newOwnerSurname, StreetAddress newLocation)
        {
            this.FranchiseId = franchiseId;
            this.SellingDate = sellingDate;
            this.NewCorporateName = newCorporateName;
            this.NewOwnerGiveName = newOwnerGiveName;
            this.NewOwnerSurname = newOwnerSurname;
            this.MovingToLocation = newLocation;
        }
        public string FranchiseId { get; }
        public DateTime SellingDate { get; }
        public string NewCorporateName { get; }
        public string NewOwnerGiveName { get; }
        public string NewOwnerSurname { get; }
        public StreetAddress MovingToLocation { get; }
    }
}
