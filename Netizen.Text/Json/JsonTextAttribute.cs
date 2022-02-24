using System;

namespace Netizen.Text.Json
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
