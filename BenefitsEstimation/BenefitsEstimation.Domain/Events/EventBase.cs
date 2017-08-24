using d60.Cirqus.Events;

namespace Benefits.Domain.Events
{
    public abstract class EventBase: DomainEvent<BenefitEstimateRoot>
    {
        protected EventBase(string id):base()
        {
            this.RootId = id;
        }

        public string RootId { get; protected set; }
    }
}
