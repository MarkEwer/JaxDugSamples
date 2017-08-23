namespace Benefits.Domain.Commands
{
    public class AddDependentToBenefitsEstimate : AddSpouseToBenefitsEstimate
    {
        public AddDependentToBenefitsEstimate(string id, string firstName, string lastName) : base(id, firstName, lastName) { }
    }
}
