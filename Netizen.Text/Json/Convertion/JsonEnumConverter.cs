using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Netizen.Text.Json.Convertion
{
    /// <summary>
    /// 枚举转换器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonEnumConverter<T> : JsonConverter<T> where T : struct
    {
        public Dictionary<string, string> NameMap { get; private set; }
        public Dictionary<string, string> TextMap { get; private set; }
        public JsonEnumConverter()
        {
            Type type = typeof(T);
            NameMap = type.GetFields()
                .SelectMany(fi => fi.GetCustomAttributes(true)
                    .Where(i => i is JsonTextAttribute)
                    .Select(i => new KeyValuePair<string, string>(fi.Name, (i as JsonTextAttribute).Text))
                )
                .ToDictionary(p => p.Key, p => p.Value);
            TextMap = type.GetFields()
                .SelectMany(fi => fi.GetCustomAttributes(true)
                    .Where(i => i is JsonTextAttribute)
                    .Select(i => new KeyValuePair<string, string>((i as JsonTextAttribute).Text, fi.Name))
                )
                .ToDictionary(p => p.Key, p => p.Value);
        }

        public override T Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            string text = reader.GetString();
            if (TextMap.ContainsKey(text))
            {
                text = TextMap[text];
            }
            return Enum.Parse<T>(text);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            string text = value.ToString();
            if (NameMap.ContainsKey(text))
            {
                text = NameMap[text];
            }
            writer.WriteStringValue(text);
        }
    }
}
