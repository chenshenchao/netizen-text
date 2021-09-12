using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Netizen.Text
{
    public enum NamingStyle
    {
        CamelCase,
        KebabCase,
        SnakeCase,
        PascalCase,
    }

    /// <summary>
    /// 命令风格扩展
    /// </summary>
    public static class NamingStyleExtends
    {
        private static Regex UpPattern = new Regex("[A-Z]([^A-Z]|$)");
        private static Regex SpanPattern = new Regex("[ _-]+");

        /// <summary>
        /// 获取指定命名风格的字符串。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static string To(this string source, NamingStyle style)
        {
            string temp = UpPattern.Replace(source, "_$0");
            string[] group = SpanPattern.Replace(temp, "_")
                .Split('_')
                .Where(t => !string.IsNullOrEmpty(t))
                .ToArray();
            switch (style)
            {
                case NamingStyle.CamelCase:
                    return string.Join(string.Empty, group.Select((t, j) => j > 0 ? t.ToFirstUpper() : t.ToLower()));
                case NamingStyle.KebabCase:
                    return string.Join('-', group.Select(t => t.ToLower()));
                case NamingStyle.SnakeCase:
                    return string.Join('_', group.Select(t => t.ToLower()));
                case NamingStyle.PascalCase:
                    return string.Join(string.Empty, group.Select(t => t.ToFirstUpper()));
            }
            return string.Join(' ', group.Select((t, j) => j > 0 ? t.ToLower() : t.ToFirstUpper()));
        }

        /// <summary>
        /// 首字母大写。
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToFirstUpper(this string source)
        {
            char[] group = source.ToArray();
            group[0] = char.ToUpper(group[0]);
            return new string(group);
        }
    }
}
