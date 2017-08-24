using System;
using d60.Cirqus.Commands;

namespace Benefits.Domain.Commands
{
    public class AddSpouseToBenefitsEstimate:Command<BenefitEstimateRoot>
    {
        public AddSpouseToBenefitsEstimate(string id, string firstName, string lastName):base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; protected set;}
        public string LastName { get; protected set; }

        public override void Execute(BenefitEstimateRoot aggregateRoot)
        {
            aggregateRoot.AddSpouse(this);
        }
    }
}
