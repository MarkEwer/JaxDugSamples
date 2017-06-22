using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SensorEventEventType
    {
        [System.Runtime.Serialization.EnumMember(Value = "sensorEvent")]
        SensorEvent = 0,

    }
}
