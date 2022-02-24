using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Netizen.Text.Json;

namespace Netizen.Text.Demo
{
    [JsonEnum]
    public enum DemoGender : byte
    {
        [JsonText("男性")]
        Male,

        [JsonText("女性")]
        Female,
    }
}
