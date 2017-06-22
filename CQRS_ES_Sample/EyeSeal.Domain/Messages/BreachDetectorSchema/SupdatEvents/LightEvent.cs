using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    public class LightEvent : EventBase
    {
        [Newtonsoft.Json.JsonProperty("eventType", Required = Newtonsoft.Json.Required.Always)]
        public LightEventEventType EventType { get; set; }

        [Newtonsoft.Json.JsonProperty("eventno", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Eventno { get; set; }

        [Newtonsoft.Json.JsonProperty("lights", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public LightsSchema Lights { get; set; }
        public static LightEvent FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<LightEvent>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
        public override bool IsAlert()
        {
            return ( Lights.Lit.HasValue && Lights.Lit.Value ) 
                  || Lights.Left.Lit
                  || Lights.Right.Lit
                  || Lights.Right.Lit;
        }
    }
}
