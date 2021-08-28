using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Netizen.Text
{
    public class JsonDateTimeConverter : JsonConverter<DateTime>
    {
        public string Format { get; private set; }

        public JsonDateTimeConverter(string format="yyyy-MM-dd HH:mm:ss")
        {
            Format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }

    public class JsonDateTimeNullableConverter : JsonConverter<DateTime?>
    {
        public string Format { get; private set; }

        public JsonDateTimeNullableConverter(string format = "yyyy-MM-dd HH:mm:ss")
        {
            Format = format;
        }

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime result;
            return DateTime.TryParse(reader.GetString(), out result) ? result: null;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value?.ToString(Format));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
