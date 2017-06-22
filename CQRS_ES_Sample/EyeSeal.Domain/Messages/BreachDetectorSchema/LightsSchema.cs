using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    /// <summary>Represents the collective state of the light conditions in the container through the lit propert.  May also include the individual status of each of the active light sensors.</summary>
    public partial class LightsSchema
    {
        [Newtonsoft.Json.JsonProperty("lit", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Lit { get; set; }

        /// <summary>number (from 1) of the light lit/unlit cycle this event belongs to</summary>
        [Newtonsoft.Json.JsonProperty("event", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Event { get; set; } = 0;

        [Newtonsoft.Json.JsonProperty("left", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Lightpos Left { get; set; }

        [Newtonsoft.Json.JsonProperty("right", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Lightpos Right { get; set; }

        [Newtonsoft.Json.JsonProperty("rear", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Lightpos Rear { get; set; }

        [Newtonsoft.Json.JsonProperty("frontpass", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Frontpass { get; set; }

        [Newtonsoft.Json.JsonProperty("rearpass", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Rearpass { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static LightsSchema FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<LightsSchema>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
