using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Netizen.Text.Json.Convertion
{
    /// <summary>
    /// Json 时间序列化转换器
    /// 默认格式 yyyy-MM-dd HH:mm:ss
    /// </summary>
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
}
