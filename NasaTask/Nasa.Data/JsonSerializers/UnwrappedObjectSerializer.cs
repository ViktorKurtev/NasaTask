using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Nasa.Data.JsonSerializers
{
    /// <summary>
    /// Json Serializer that recursively pulls out properties from nested classes to the top of their hierarchy.
    /// </summary>
    public class UnwrappedObjectSerializer : JsonConverter
    {
        private readonly bool shouldAppendParentName;

        /// <summary>
        /// Creates a new instance of the serializer.
        /// </summary>
        /// <param name="shouldAppendParentName">Should the parent name of nested classes be appended to their front or not. 
        /// If collisions are expected this should be true. If for example 2 nested properties have the same name and they are attempted
        /// to be pulled out to the top of their hierarchy, the second one will have precedence and the data of the first one will be lost.
        /// If set to true, the parent name will be appended to its front recursively, preventing naming collisions.</param>
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

        /// <summary>
        /// Recursively flatten the properties of an object, pulling out nested properties to the top of the hierarchy.
        /// </summary>
        /// <param name="writer">JsonWriter to write the result to.</param>
        /// <param name="jProperty">The property currently being flattened.</param>
        /// <param name="nestingLevel">How many nested levels we've gone down. Used to determine how many parent names levels
        /// we need to append (if set to true in constructor).</param>
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

        /// <summary>
        /// Writes a property name and value to a json writer.
        /// </summary>
        /// <param name="writer">JsonWriter to write to.</param>
        /// <param name="jProperty">Current property being written.</param>
        /// <param name="nestingLevel">How many nested levels we've gone down, used to append parent names (if true in constructor).</param>
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
