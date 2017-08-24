namespace Benefits.Domain.Events
{
    public class DependentAdded : SpouseAdded
    {
        public DependentAdded(string id, string firstName, string lastName) : base(id, firstName, lastName) { }
    }
}
