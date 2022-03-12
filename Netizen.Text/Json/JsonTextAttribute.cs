using System;

namespace Netizen.Text.Json
{
    /// <summary>
    /// Json 序列化特性，用于标注枚举。
    /// </summary>
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
