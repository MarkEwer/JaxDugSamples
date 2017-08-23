namespace Benefits.Domain.Events
{
    public class SpouseAdded:EventBase
    {
        public SpouseAdded(string id, string firstName, string lastName):base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
    }
}
