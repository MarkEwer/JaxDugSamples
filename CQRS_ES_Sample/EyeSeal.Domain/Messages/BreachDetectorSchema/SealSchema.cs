using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    /// <summary>Container seal status.</summary>
    public partial class SealSchema
    {
        /// <summary>True if the container seal is confirmed</summary>
        [Newtonsoft.Json.JsonProperty("sealed", Required = Newtonsoft.Json.Required.Always)]
        public bool Sealed { get; set; } = false;

        /// <summary>Location and time of last door closing before seal confirmed</summary>
        [Newtonsoft.Json.JsonProperty("closed", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Sealstate Closed { get; set; }

        /// <summary>Location and time when seal confirmed</summary>
        [Newtonsoft.Json.JsonProperty("confirmed", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Sealstate Confirmed { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static SealSchema FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SealSchema>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
