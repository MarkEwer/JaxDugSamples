namespace Benefits.Domain.Events
{
    public class SpouseRemoved:SpouseAdded
    {
        public SpouseRemoved(string id, string firstName, string lastName) : base(id, firstName, lastName) { }
    }
}
