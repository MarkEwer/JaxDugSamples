using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;
using System;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    public partial class BreachDetectorData
    {
        public Guid Id { get; set; }

        [Newtonsoft.Json.JsonProperty("timeval", Required = Newtonsoft.Json.Required.Always)]
        public int Timeval { get; set; }

        [Newtonsoft.Json.JsonProperty("lognum", Required = Newtonsoft.Json.Required.Always)]
        public int Lognum { get; set; }

        [Newtonsoft.Json.JsonProperty("tempf", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Tempf { get; set; }

        [Newtonsoft.Json.JsonProperty("humidity", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Humidity { get; set; }

        [Newtonsoft.Json.JsonProperty("bds", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public BdsSchema Bds { get; set; }

        [Newtonsoft.Json.JsonProperty("radio", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public RadioSchema Radio { get; set; }

        [Newtonsoft.Json.JsonProperty("location", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public LocSchema Location { get; set; }

        [Newtonsoft.Json.JsonProperty("doors", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DoorsSchema Doors { get; set; }

        [Newtonsoft.Json.JsonProperty("lights", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public LightsSchema Lights { get; set; }

        [Newtonsoft.Json.JsonProperty("battery", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public BatterySchema Battery { get; set; }

        [Newtonsoft.Json.JsonProperty("tdata", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public TdataSchema Tdata { get; set; }

        [Newtonsoft.Json.JsonProperty("seal", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public SealSchema Seal { get; set; }

        [Newtonsoft.Json.JsonProperty("log", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<LogSchema> Log { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static BreachDetectorData FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<BreachDetectorData>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
