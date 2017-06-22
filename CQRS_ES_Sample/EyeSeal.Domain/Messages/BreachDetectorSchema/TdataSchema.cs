
using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    /// <summary>Timing data for various connection parameters.</summary>
    public partial class TdataSchema
    {
        /// <summary>Time, in seconds, for GSM registration</summary>
        [Newtonsoft.Json.JsonProperty("gsmr", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Gsmr { get; set; } = 0;

        /// <summary>Time, in milliseconds, for battery to return to 3.0 volts</summary>
        [Newtonsoft.Json.JsonProperty("recovery", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Recovery { get; set; } = 0;

        /// <summary>Time, in seconds, that the radio power has been active</summary>
        [Newtonsoft.Json.JsonProperty("conn", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Conn { get; set; } = 0;

        /// <summary>Time, in seconds, for GPRS registration to complete</summary>
        [Newtonsoft.Json.JsonProperty("gprsr", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Gprsr { get; set; } = 0;

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static TdataSchema FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TdataSchema>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
