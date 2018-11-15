using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SLOBSharp.Domain.Mapping
{
    internal class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType) => objectType == typeof(List<T>);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Boolean)
            {
                return new List<T>();
            }

            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }

            return new List<T> { token.ToObject<T>() };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var enumerable = value as IEnumerable<T>;
            var list = enumerable.ToList();
            if (list.Count == 1)
            {
                value = list[0];
            }

            serializer.Serialize(writer, value);
        }
    }
}
