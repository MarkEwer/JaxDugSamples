using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EepromEventEventType
    {
        [System.Runtime.Serialization.EnumMember(Value = "eepromEvent")]
        EepromEvent = 0,

    }
}
