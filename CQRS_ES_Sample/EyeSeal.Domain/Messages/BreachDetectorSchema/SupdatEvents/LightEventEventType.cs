using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LightEventEventType
    {
        [System.Runtime.Serialization.EnumMember(Value = "lightEvent")]
        LightEvent = 0,

    }
}
