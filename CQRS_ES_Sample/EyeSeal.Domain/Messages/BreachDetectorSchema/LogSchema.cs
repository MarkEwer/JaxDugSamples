using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;
using EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    /// <summary>This defines each of the items in the log[] array.  Note that this element uses the JSON Schema construct called oneOf to enforce that there must be one event and that the event must be one of the event types defined above.</summary>
    public partial class LogSchema
    {
        /// <summary>Index to the EEPROM storage location, -1 if not stored in EEPROM</summary>
        [Newtonsoft.Json.JsonProperty("index", Required = Newtonsoft.Json.Required.Always)]
        public int Index { get; set; } = 0;

        /// <summary>Time of detection in seconds since midnight 1/1/2001</summary>
        [Newtonsoft.Json.JsonProperty("timeval", Required = Newtonsoft.Json.Required.Always)]
        public int Timeval { get; set; } = 0;

        /// <summary>Message type number</summary>
        [Newtonsoft.Json.JsonProperty("errnum", Required = Newtonsoft.Json.Required.Always)]
        public int Errnum { get; set; } = 0;

        /// <summary>Text description of message type</summary>
        [Newtonsoft.Json.JsonProperty("errdesc", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string Errdesc { get; set; }

        [Newtonsoft.Json.JsonProperty("supdat", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public EventBase Supdat { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static LogSchema FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<LogSchema>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
