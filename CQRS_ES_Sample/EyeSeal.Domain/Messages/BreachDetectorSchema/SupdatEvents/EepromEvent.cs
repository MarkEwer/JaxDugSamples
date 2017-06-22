using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    public class EepromEvent : EventBase
    {
        [Newtonsoft.Json.JsonProperty("eventType", Required = Newtonsoft.Json.Required.Always)]
        public EepromEventEventType EventType { get; set; }

        [Newtonsoft.Json.JsonProperty("code", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Code { get; set; }
        public static EepromEvent FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<EepromEvent>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
        public override bool IsAlert()
        {
            return false;
        }
    }
}
