using Akka.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benefits.Domain
{
    public class BenefitEstimateManagerActor : ReceivePersistentActor
    {
        public override string PersistenceId => "BenefitEstimateManager";
    }
}
