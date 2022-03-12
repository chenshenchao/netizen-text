using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Netizen.Text.Json.Convertion;

namespace Netizen.Text.Json
{
    public static class JsonExtends
    {
        public static JsonSerializerOptions DefaultSerializerOptions { get; private set; }

        /// <summary>
        /// 初始化，配置几个默认的转换规则。
        /// </summary>
        static JsonExtends()
        {
            DefaultSerializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            DefaultSerializerOptions.Converters.Add(new JsonDateTimeConverter());
            DefaultSerializerOptions.Converters.Add(new JsonDateTimeNullableConverter());
            DefaultSerializerOptions.Converters.Add(new JsonEnumConverterFactory());
        }

        /// <summary>
        /// Json序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="one"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T one)
        {
            return JsonSerializer.Serialize(one, DefaultSerializerOptions);
        }

        /// <summary>
        /// 异步Json序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="one"></param>
        /// <returns></returns>
        public static async Task<string> ToJsonAsync<T>(this T one)
        {
            MemoryStream stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, one, DefaultSerializerOptions);
            return stream.ToString();
        }

        /// <summary>
        /// Json 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static T JsonAs<T>(this string text)
        {
            return JsonSerializer.Deserialize<T>(text, DefaultSerializerOptions);
        }

        /// <summary>
        /// 异步 Json 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static async ValueTask<T> JsonAsAsync<T>(this string text)
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(text));
            return await JsonSerializer.DeserializeAsync<T>(stream, DefaultSerializerOptions);
        }
    }
}
