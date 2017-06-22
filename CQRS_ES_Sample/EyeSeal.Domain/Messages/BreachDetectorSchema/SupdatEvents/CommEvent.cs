using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    public class CommEvent : EventBase
    {
        [Newtonsoft.Json.JsonProperty("eventType", Required = Newtonsoft.Json.Required.Always)]
        public CommEventEventType EventType { get; set; }

        [Newtonsoft.Json.JsonProperty("conncount", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Conncount { get; set; }

        [Newtonsoft.Json.JsonProperty("tempf", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Tempf { get; set; }

        [Newtonsoft.Json.JsonProperty("humidity", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Humidity { get; set; }

        [Newtonsoft.Json.JsonProperty("msgnum", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Msgnum { get; set; }

        [Newtonsoft.Json.JsonProperty("msg", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Msg { get; set; }
        public static CommEvent FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<CommEvent>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
        public override bool IsAlert()
        {
            return false;
        }
    }
}
