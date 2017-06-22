using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DeepdarkEventEventType
    {
        [System.Runtime.Serialization.EnumMember(Value = "deepdarkEvent")]
        DeepdarkEvent = 0,

    }
}
