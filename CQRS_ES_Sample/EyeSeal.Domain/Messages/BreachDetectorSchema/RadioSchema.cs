using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    /// <summary>Radio operation parameters</summary>
    public partial class RadioSchema
    {
        /// <summary>Received signal strength 0 = unknown</summary>
        [Newtonsoft.Json.JsonProperty("rssi", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Rssi { get; set; } = 0;

        /// <summary>International Mobile Equipment Identifier (Radio Serial Number)</summary>
        [Newtonsoft.Json.JsonProperty("imei", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Imei { get; set; } = "";

        /// <summary>Revision String for the radio</summary>
        [Newtonsoft.Json.JsonProperty("vers", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Vers { get; set; } = "";

        /// <summary>Bit Error Rate 0=no errors, 7=12.8% or more, 99=unknown</summary>
        [Newtonsoft.Json.JsonProperty("bert", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Bert { get; set; } = 99;

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static RadioSchema FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RadioSchema>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
