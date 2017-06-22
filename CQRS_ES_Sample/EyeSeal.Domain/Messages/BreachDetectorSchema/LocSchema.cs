using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    /// <summary>Identification information for a cell tower connection.  Valid only when mnc and mcc are non-zero</summary>
    public partial class LocSchema
    {
        /// <summary>Tower location Mobile Country Code</summary>
        [Newtonsoft.Json.JsonProperty("mcc", Required = Newtonsoft.Json.Required.Always)]
        public int Mcc { get; set; } = 0;

        /// <summary>Tower location Mobile Network Code</summary>
        [Newtonsoft.Json.JsonProperty("mnc", Required = Newtonsoft.Json.Required.Always)]
        public int Mnc { get; set; } = 0;

        /// <summary>Tower location Location Area Code</summary>
        [Newtonsoft.Json.JsonProperty("lac", Required = Newtonsoft.Json.Required.Always)]
        public int Lac { get; set; } = 0;

        /// <summary>Tower location Cell ID</summary>
        [Newtonsoft.Json.JsonProperty("cid", Required = Newtonsoft.Json.Required.Always)]
        public int Cid { get; set; } = 0;

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static LocSchema FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<LocSchema>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
