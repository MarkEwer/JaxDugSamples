using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FatEventEventType
    {
        [System.Runtime.Serialization.EnumMember(Value = "fatEvent")]
        FatEvent = 0,

    }
}
