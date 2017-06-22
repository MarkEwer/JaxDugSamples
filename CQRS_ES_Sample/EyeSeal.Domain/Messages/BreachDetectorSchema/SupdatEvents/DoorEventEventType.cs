using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DoorEventEventType
    {
        [System.Runtime.Serialization.EnumMember(Value = "doorEvent")]
        DoorEvent = 0,

    }
}
