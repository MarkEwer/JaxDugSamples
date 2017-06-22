using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    public partial class Doorpos
    {
        [Newtonsoft.Json.JsonProperty("open", Required = Newtonsoft.Json.Required.Always)]
        public bool Open { get; set; }

        /// <summary>Raw count from door sensor</summary>
        [Newtonsoft.Json.JsonProperty("count", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Count { get; set; } = 0;

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static Doorpos FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Doorpos>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
