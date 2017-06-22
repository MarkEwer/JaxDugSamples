using EyeSeal.Domain.Messages.BreachDetectorSchema.SupdatEvents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyeSeal.Domain.Messages.BreachDetectorSchema.Converters
{
    public class EventBaseConverter : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings { ContractResolver = new EventBaseSpecifiedConcreteClassConverter() };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(EventBase));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            switch (jo["eventType"].Value<string>())
            {
                case "clockEvent":
                    return JsonConvert.DeserializeObject<ClockEvent>(jo.ToString(), SpecifiedSubclassConversion);
                case "commEventSuccess":
                case "commEventError":
                    return JsonConvert.DeserializeObject<CommEvent>(jo.ToString(), SpecifiedSubclassConversion);
                case "doorEvent":
                    return JsonConvert.DeserializeObject<DoorEvent>(jo.ToString(), SpecifiedSubclassConversion);
                case "lightEvent":
                    return JsonConvert.DeserializeObject<LightEvent>(jo.ToString(), SpecifiedSubclassConversion);
                case "powerEvent":
                    return JsonConvert.DeserializeObject<PowerEvent>(jo.ToString(), SpecifiedSubclassConversion);
                case "relocatedEvent":
                    return JsonConvert.DeserializeObject<RelocatedEvent>(jo.ToString(), SpecifiedSubclassConversion);
                case "eepromEvent":
                    return JsonConvert.DeserializeObject<EepromEvent>(jo.ToString(), SpecifiedSubclassConversion);
                case "deepdarkEvent":
                    return JsonConvert.DeserializeObject<DeepdarkEvent>(jo.ToString(), SpecifiedSubclassConversion);
                case "fatEvent":
                    return JsonConvert.DeserializeObject<FatEvent>(jo.ToString(), SpecifiedSubclassConversion);
                case "sensorEvent":
                    return JsonConvert.DeserializeObject<SensorEvent>(jo.ToString(), SpecifiedSubclassConversion);
                default:
                    throw new JsonException($"Failed To Deserialize because eventType was '{jo["eventType"].Value<string>()}'");
            }
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var t = JToken.FromObject(value);
            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                var o = (JObject) t;
                IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();
                o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));
                o.WriteTo(writer);
            }
        }
    }
}
