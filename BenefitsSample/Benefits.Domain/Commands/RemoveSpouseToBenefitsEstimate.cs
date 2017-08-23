namespace Benefits.Domain.Commands
{
    public class RemoveSpouseToBenefitsEstimate : AddSpouseToBenefitsEstimate
    {
        public RemoveSpouseToBenefitsEstimate(string id, string firstName, string lastName) : base(id, firstName, lastName) { }
    }
}
