using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    /// <summary>Status of the BDS firmware</summary>
    public partial class BdsSchema
    {
        /// <summary>Firmware version number</summary>
        [Newtonsoft.Json.JsonProperty("vers", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Vers { get; set; } = "";

        /// <summary>The index value for the last occupied log entry in the EEPROM event log</summary>
        [Newtonsoft.Json.JsonProperty("lastindex", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Lastindex { get; set; } = 0;

        /// <summary>The device identification string</summary>
        [Newtonsoft.Json.JsonProperty("unitid", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Unitid { get; set; } = "";

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static BdsSchema FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<BdsSchema>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
