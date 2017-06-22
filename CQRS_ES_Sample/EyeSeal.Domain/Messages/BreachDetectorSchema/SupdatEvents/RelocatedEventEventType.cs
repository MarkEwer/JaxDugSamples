using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RelocatedEventEventType
    {
        [System.Runtime.Serialization.EnumMember(Value = "relocatedEvent")]
        RelocatedEvent = 0,
    }
}
