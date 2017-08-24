namespace Benefits.Domain.Events
{
    public class DependentRemoved : SpouseAdded
    {
        public DependentRemoved(string id, string firstName, string lastName) : base(id, firstName, lastName) { }
    }
}
