using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    public class DoorEvent : EventBase
    {
        [Newtonsoft.Json.JsonProperty("eventType", Required = Newtonsoft.Json.Required.Always)]
        public DoorEventEventType EventType { get; set; }

        [Newtonsoft.Json.JsonProperty("eventno", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Eventno { get; set; }

        [Newtonsoft.Json.JsonProperty("doors", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DoorsSchema Doors { get; set; }
        public static DoorEvent FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DoorEvent>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
        public override bool IsAlert()
        {
            return Doors.Left.Open || Doors.Right.Open;
        }
    }
}
