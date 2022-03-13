using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Netizen.Text.Json;
using Netizen.Text.Demo.Properties;

namespace Netizen.Text.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadOnlySpan<byte> utf8Bom = new byte[] { 0xEF, 0xBB, 0xBF };
            ReadOnlySpan<byte> r1 = Resources._1;
            ReadOnlySpan<byte> r = r1.StartsWith(utf8Bom) ? r1.Slice(utf8Bom.Length) : r1;
            JsonDocument d = JsonDocument.Parse(r.ToArray());
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            var fs = d.Flat();
            sw.Stop();
            Console.WriteLine("elapsed: {0}ms", sw.ElapsedMilliseconds);
            foreach (var f in fs)
            {
                var v = f.Value is List<object> ? (f.Value as List<object>).FormatString() : f.Value.ToString();
                Console.WriteLine("{0} => {1}", f.Key, v);
            }
        }
    }
}
