using Server.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Events.Franchise
{
    public class FranchiseMoved
    {
        public FranchiseMoved(string franchiseId, StreetAddress location)
        {
            this.FranchiseId = franchiseId;
            this.NewMailingAddress = location;
        }
        public string FranchiseId { get; }
        public StreetAddress NewMailingAddress { get; }
    }
}
