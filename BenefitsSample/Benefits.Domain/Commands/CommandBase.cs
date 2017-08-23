namespace Benefits.Domain.Commands
{
    public abstract class CommandBase
    {
        protected CommandBase(string id)
        {
            this.PersistenceId = id;
        }

        public string PersistenceId { get; protected set; }
    }
}
