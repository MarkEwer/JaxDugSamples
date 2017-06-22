using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    /// <summary>Represents the collective state of the container doors through the open property.  May contain the individual state of the left and right doors.</summary>
    public partial class DoorsSchema
    {
        [Newtonsoft.Json.JsonProperty("open", Required = Newtonsoft.Json.Required.Always)]
        public bool Open { get; set; }

        /// <summary>number (from 1) of the door open/closed cycle this event belongs to</summary>
        [Newtonsoft.Json.JsonProperty("event", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Event { get; set; } = 0;

        [Newtonsoft.Json.JsonProperty("left", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Doorpos Left { get; set; }

        [Newtonsoft.Json.JsonProperty("right", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Doorpos Right { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static DoorsSchema FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DoorsSchema>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
