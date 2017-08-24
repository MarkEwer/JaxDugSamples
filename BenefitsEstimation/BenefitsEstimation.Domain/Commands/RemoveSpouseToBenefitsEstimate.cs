using System;

namespace Benefits.Domain.Commands
{
    public class RemoveSpouseToBenefitsEstimate : AddSpouseToBenefitsEstimate
    {
        public RemoveSpouseToBenefitsEstimate(string id, string firstName, string lastName) : base(id, firstName, lastName) { }

        public override void Execute(BenefitEstimateRoot aggregateRoot)
        {
            aggregateRoot.RemoveSpouse(this);
        }
    }
}
