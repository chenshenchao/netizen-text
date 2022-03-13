using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Netizen.Text.Json.Flation;

namespace Netizen.Text.Json
{
    public static class JsonDocumentExtends
    {
        public static JsonFlattener Flattener { get; private set; }

        static JsonDocumentExtends()
        {
            Flattener = new JsonFlattener();
        }

        public static Dictionary<string, object> Flat(this JsonDocument document)
        {
            return Flattener.Flat(document);
        }
    }
}
