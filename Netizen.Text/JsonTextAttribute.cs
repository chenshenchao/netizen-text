using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netizen.Text
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class JsonTextAttribute : Attribute
    {
        public string Text { get; private set; }
        public JsonTextAttribute(string text)
        {
            Text = text;
        }
    }
}
