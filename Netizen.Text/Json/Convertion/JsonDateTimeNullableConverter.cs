using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Netizen.Text.Json.Convertion
{
    /// <summary>
    /// Json 可空时间序列化转换器
    /// 默认格式 yyyy-MM-dd HH:mm:ss
    /// </summary>
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
            return DateTime.TryParse(reader.GetString(), out result) ? result : null;
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
