using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Netizen.Text.Json.Convertion
{
    public class JsonEnumConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = type.GetGenericArguments()[0];
            }
            return type.IsEnum && type.GetCustomAttributes(true)
                    .Where(i => i is JsonEnumAttribute)
                    .Select(i => i as JsonEnumAttribute)
                    .Count() > 0;
        }

        public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Type ptype = type.GetGenericArguments()[0];
                Type gtype = typeof(JsonEnumNullableConverter<>).MakeGenericType(ptype);
                return Activator.CreateInstance(gtype) as JsonConverter;
            }
            Type ctype = typeof(JsonEnumConverter<>).MakeGenericType(type);
            return Activator.CreateInstance(ctype) as JsonConverter;
        }
    }
}
