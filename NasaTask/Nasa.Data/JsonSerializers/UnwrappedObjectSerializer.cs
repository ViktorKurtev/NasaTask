using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Nasa.Data.JsonSerializers
{
    public class UnwrappedObjectSerializer : JsonConverter
    {
        private readonly bool shouldAppendParentName;

        public UnwrappedObjectSerializer(bool shouldAppendParentName)
        {
            this.shouldAppendParentName = shouldAppendParentName;
        }

        public override bool CanConvert(Type objectType) => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                writer.WriteStartArray();

                foreach (var child in t)
                {
                    WriteJson(writer, child, serializer);
                }

                writer.WriteEndArray();
            }
            else
            {
                JObject o = (JObject)t;

                writer.WriteStartObject();

                foreach (var property in o.Properties())
                {
                    RecursivelyFlattenProperties(writer, property, 0);
                }

                writer.WriteEndObject();
            }
        }

        private void RecursivelyFlattenProperties(JsonWriter writer, JProperty jProperty, int nestingLevel)
        {
            var subProp = jProperty.First();

            if (subProp.Children().Any())
            {
                foreach (var token in subProp.Children())
                {
                    if (token is JObject)
                    {
                        foreach (var tokenChild in (token as JObject).Children())
                        {
                            RecursivelyFlattenProperties(writer, tokenChild as JProperty, nestingLevel + 1);
                        }
                    }
                    else
                    {
                        RecursivelyFlattenProperties(writer, token as JProperty, nestingLevel + 1);
                    }
                }
            }
            else
            {
                WritePropToWriter(writer, subProp.Parent as JProperty, nestingLevel);
            }
        }

        private void WritePropToWriter(JsonWriter writer, JProperty jProperty, int nestingLevel)
        {
            var propName = jProperty.Name;

            var propVal = jProperty.Value;

            if (shouldAppendParentName)
            {
                var pathCollection = jProperty.Path.Split('.');

                for (int i = nestingLevel - 1; i >= 0; i--)
                {
                    propName = $"{pathCollection[i]}_{propName}";
                }
            }

            writer.WritePropertyName(propName);
            writer.WriteValue(propVal);
        }
    }
}
