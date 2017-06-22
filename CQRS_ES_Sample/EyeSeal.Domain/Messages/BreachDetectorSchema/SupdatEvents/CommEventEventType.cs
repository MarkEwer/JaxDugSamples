using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommEventEventType
    {
        [System.Runtime.Serialization.EnumMember(Value = "commEventSuccess")]
        CommEventSuccess = 0,

        [System.Runtime.Serialization.EnumMember(Value = "commEventError")]
        CommEventError = 1,

    }
}
