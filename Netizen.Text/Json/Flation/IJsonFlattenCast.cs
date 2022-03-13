using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Netizen.Text.Json.Flation
{
    public interface IJsonFlattenCast
    {
        public object Cast(JsonElement element);
    }
}
