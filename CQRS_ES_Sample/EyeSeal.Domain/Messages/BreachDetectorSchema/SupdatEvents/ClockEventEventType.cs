using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ClockEventEventType
    {
        [System.Runtime.Serialization.EnumMember(Value = "clockEvent")]
        ClockEvent = 0,

    }
}
