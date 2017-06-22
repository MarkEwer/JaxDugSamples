using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PowerEventEventType
    {
        [System.Runtime.Serialization.EnumMember(Value = "powerEvent")]
        PowerEvent = 0,

    }
}
