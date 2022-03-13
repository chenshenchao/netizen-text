using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Netizen.Text.Json.Flation
{
    /// <summary>
    /// 默认压平数值转换器
    /// </summary>
    public class JsonFlattenDefaultCast : IJsonFlattenCast
    {
        public object Cast(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Null:
                case JsonValueKind.Undefined:
                    return null;
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.False:
                    return false;
                case JsonValueKind.Number:
                    return element.GetDecimal();
                case JsonValueKind.String:
                    return element.GetString();
            }
            return null;
        }
    }
}
