using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    public class PowerEvent : EventBase
    {
        [Newtonsoft.Json.JsonProperty("eventType", Required = Newtonsoft.Json.Required.Always)]
        public PowerEventEventType EventType { get; set; }

        [Newtonsoft.Json.JsonProperty("msg", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Msg { get; set; }
        public static PowerEvent FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<PowerEvent>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
        public override bool IsAlert()
        {
            return false;
        }
    }
}
