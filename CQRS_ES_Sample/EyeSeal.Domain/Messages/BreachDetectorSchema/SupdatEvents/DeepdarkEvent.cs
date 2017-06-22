using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    public class DeepdarkEvent : EventBase
    {
        [Newtonsoft.Json.JsonProperty("eventType", Required = Newtonsoft.Json.Required.Always)]
        public DeepdarkEventEventType EventType { get; set; }

        [Newtonsoft.Json.JsonProperty("lit", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Lit { get; set; }
        public static DeepdarkEvent FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DeepdarkEvent>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
        public override bool IsAlert()
        {
            return true;
        }
    }
}
