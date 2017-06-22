using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    public class FatEvent : EventBase
    {
        [Newtonsoft.Json.JsonProperty("eventType", Required = Newtonsoft.Json.Required.Always)]
        public FatEventEventType EventType { get; set; }

        [Newtonsoft.Json.JsonProperty("fatvalue", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Fatvalue { get; set; }
        public static FatEvent FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<FatEvent>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
        public override bool IsAlert()
        {
            return false;
        }
    }
}
