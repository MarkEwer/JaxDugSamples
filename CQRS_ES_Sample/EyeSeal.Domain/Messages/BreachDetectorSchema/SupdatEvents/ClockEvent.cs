using System;
using EyeSeal.Domain.Messages.BreachDetectorSchema.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    public class ClockEvent : EventBase
    {
        [Newtonsoft.Json.JsonProperty("eventType", Required = Newtonsoft.Json.Required.Always)]
        public ClockEventEventType EventType { get; set; }

        /// <summary>The number of seconds that the clock was adjusted.</summary>
        [Newtonsoft.Json.JsonProperty("timediff", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Timediff { get; set; } = 0;
        public static ClockEvent FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ClockEvent>(data, new Newtonsoft.Json.JsonConverter[] { new EventBaseConverter() });
        }
        public override bool IsAlert()
        {
            return false;
        }
    }
}
