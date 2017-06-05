using Server.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands.Franchise
{
    public class MoveFranchise
    {
        public MoveFranchise(string franchiseId, StreetAddress newLocation)
        {
            this.FranchiseId = franchiseId;
            this.MovingToLocation = newLocation;
        }
        public string FranchiseId { get; }
        public StreetAddress MovingToLocation { get; }
    }
}
