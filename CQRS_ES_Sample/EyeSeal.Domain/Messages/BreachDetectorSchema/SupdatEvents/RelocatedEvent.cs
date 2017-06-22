using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    public class RelocatedEvent : EventBase
    {
        [Newtonsoft.Json.JsonProperty("eventType", Required = Newtonsoft.Json.Required.Always)]
        public RelocatedEventEventType EventType { get; set; }

        [Newtonsoft.Json.JsonProperty("location", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public LocSchema Location { get; set; }
        public static RelocatedEvent FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RelocatedEvent>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
        public override bool IsAlert()
        {
            //TODO: How do we determin if a relocation is an alert or not?
            return true;
        }
    }
}
