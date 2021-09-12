using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Netizen.Text
{
    public static class JsonExtends
    {
        public static JsonSerializerOptions DefaultSerializerOptions { get; private set; }

        static JsonExtends()
        {
            DefaultSerializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            DefaultSerializerOptions.Converters.Add(new JsonDateTimeConverter());
            DefaultSerializerOptions.Converters.Add(new JsonDateTimeNullableConverter());
            DefaultSerializerOptions.Converters.Add(new JsonEnumConverter());
        }

        public static string ToJson<T>(this T one)
        {
            return JsonSerializer.Serialize(one, DefaultSerializerOptions);
        }

        public static async Task<string> ToJsonAsync<T>(this T one)
        {
            MemoryStream stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, one, DefaultSerializerOptions);
            return stream.ToString();
        }

        public static T JsonAs<T>(this string text)
        {
            return JsonSerializer.Deserialize<T>(text, DefaultSerializerOptions);
        }

        public static async ValueTask<T> JsonAsAsync<T>(this string text)
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(text));
            return await JsonSerializer.DeserializeAsync<T>(stream, DefaultSerializerOptions);
        }
    }
}
