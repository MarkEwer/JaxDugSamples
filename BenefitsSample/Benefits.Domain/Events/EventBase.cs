namespace Benefits.Domain.Events
{
    public abstract class EventBase
    {
        protected EventBase(string id)
        {
            this.PersistenceId = id;
        }

        public string PersistenceId { get; protected set; }
    }
}
