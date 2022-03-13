using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Netizen.Text.Json.Flation
{
    public class JsonFlattener
    {
        private IJsonFlattenCast cast;

        public JsonFlattener(IJsonFlattenCast cast=null)
        {
            this.cast = cast ?? new JsonFlattenDefaultCast();
        }

        /// <summary>
        /// 压平 JSON 文档
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public Dictionary<string, object> Flat(JsonDocument document)
        {
            JsonElement element = document.RootElement;
            switch (element.ValueKind)
            {
                case JsonValueKind.Array:
                    return FlatArray(element);
                case JsonValueKind.Object:
                    return FlatObject(element);
                default:
                    return new Dictionary<string, object>()
                    {
                        { string.Empty, cast.Cast(element) },
                    };
            }
        }

        /// <summary>
        /// 压平数组
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public Dictionary<string, object> FlatArray(JsonElement element)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            List<object> self = element.EnumerateArray()
                .Where(i => i.ValueKind != JsonValueKind.Object && i.ValueKind != JsonValueKind.Array)
                .Select(i => cast.Cast(i))
                .ToList();

            // 子对象
            foreach (JsonElement e in element.EnumerateArray().Where(i => i.ValueKind == JsonValueKind.Object))
            {
                foreach (var i in FlatObject(e))
                {
                    if (result.ContainsKey(i.Key))
                    {
                        if (result[i.Key] is List<object>)
                        {
                            var r = result[i.Key] as List<object>;
                            if (i.Value is List<object>)
                            {
                                result[i.Key] = r.Concat(i.Value as List<object>);
                            }
                            else
                            {
                                r.Add(i.Value);
                            }
                        }
                        else
                        {
                            result[i.Key] = new List<object> { result[i.Key], i.Value };
                        }
                    }
                    else
                    {
                        result.Add(i.Key, i.Value);
                    }
                }
            }

            // 子数组
            foreach (JsonElement e in element.EnumerateArray().Where(i => i.ValueKind == JsonValueKind.Array))
            {
                foreach (var i in FlatArray(e))
                {
                    if (i.Key == string.Empty)
                    {
                        self.AddRange(i.Value as IEnumerable<object>);
                    }
                    else
                    {
                        result.Add($"[{i.Key}]", i.Value);
                    }
                }
            }

            if (self.Count > 0)
            {
                result.Add(string.Empty, self);
            }

            return result;
        }

        /// <summary>
        /// 压平对象
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public Dictionary<string, object> FlatObject(JsonElement element)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            foreach (var p in element.EnumerateObject())
            {
                switch (p.Value.ValueKind)
                {
                    case JsonValueKind.Object:
                        foreach (var i in FlatObject(p.Value))
                        {
                            result.Add($"{p.Name}.{i.Key}", i.Value);
                        }
                        break;
                    case JsonValueKind.Array:
                        foreach (var i in FlatArray(p.Value))
                        {
                            if (i.Key == string.Empty)
                            {
                                result.Add(p.Name, i.Value);
                            }
                            else
                            {
                                result.Add($"{p.Name}[{i.Key}]", i.Value);
                            }
                        }
                        break;
                    default:
                        result.Add(p.Name, cast.Cast(p.Value));
                        break;
                }
            }

            return result;
        }
    }
}
