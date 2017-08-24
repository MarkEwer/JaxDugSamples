using System;

namespace Benefits.Domain.Commands
{
    public class RemoveDependentToBenefitsEstimate : AddSpouseToBenefitsEstimate
    {
        public RemoveDependentToBenefitsEstimate(string id, string firstName, string lastName) : base(id, firstName, lastName) { }
        public override void Execute(BenefitEstimateRoot aggregateRoot)
        {
            aggregateRoot.RemoveDependent(this);
        }
    }
}
