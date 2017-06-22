using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema
{
    public partial class Lightpos
    {
        [Newtonsoft.Json.JsonProperty("lit", Required = Newtonsoft.Json.Required.Always)]
        public bool Lit { get; set; }

        /// <summary>Raw count from light sensor</summary>
        [Newtonsoft.Json.JsonProperty("count", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Count { get; set; } = 0;

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }

        public static Lightpos FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Lightpos>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
    }
}
