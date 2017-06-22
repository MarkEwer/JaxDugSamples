using EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.Converters
{
    public class EventBaseSpecifiedConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(EventBase).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
            return base.ResolveContractConverter(objectType);
        }
    }
}
