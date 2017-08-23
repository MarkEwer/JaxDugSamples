namespace Benefits.Domain.Commands
{
    public class AddSpouseToBenefitsEstimate:CommandBase
    {
        public AddSpouseToBenefitsEstimate(string id, string firstName, string lastName):base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; protected set;}
        public string LastName { get; protected set; }
    }
}
