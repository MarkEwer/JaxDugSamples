using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    /// <summary>Battery status during a radio connection session.</summary>
    public partial class BatterySchema
    {
        /// <summary>Current battery voltage in mV</summary>
        [Newtonsoft.Json.JsonProperty("cur", Required = Newtonsoft.Json.Required.Always)]
        public int Cur { get; set; } = 0;

        /// <summary>Minimum battery voltage during transmission in mV</summary>
        [Newtonsoft.Json.JsonProperty("min", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Min { get; set; } = 0;

        /// <summary>Maximum battery voltage during transmission in mV</summary>
        [Newtonsoft.Json.JsonProperty("max", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Max { get; set; } = 0;

        /// <summary>Average battery voltage during transmission in mV</summary>
        [Newtonsoft.Json.JsonProperty("avg", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Avg { get; set; } = 0;

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static BatterySchema FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<BatterySchema>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
