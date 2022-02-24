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
            switch (style)
            {
                case NamingStyle.CamelCase:
                    return ToCamelCase(source);
                case NamingStyle.KebabCase:
                    return ToKebabCase(source);
                case NamingStyle.SnakeCase:
                    return ToSnakeCase(source);
                case NamingStyle.PascalCase:
                    return ToPascalCase(source);
            }
            string[] words = ToWords(source);
            return string.Join(' ', words.Select((t, j) => j > 0 ? t.ToLower() : t.ToFirstUpper()));
        }

        public static string ToCamelCase(this string source)
        {
            string[] words = ToWords(source);
            return string.Join(string.Empty, words.Select((t, j) => j > 0 ? t.ToFirstUpper() : t.ToLower()));
        }

        public static string ToKebabCase(this string source)
        {
            string[] words = ToWords(source);
            return string.Join('-', words.Select(t => t.ToLower()));
        }

        public static string ToSnakeCase(this string source)
        {
            string[] words = ToWords(source);
            return string.Join('_', words.Select(t => t.ToLower()));
        }

        public static string ToPascalCase(this string source)
        {
            string[] words = ToWords(source);
            return string.Join(string.Empty, words.Select(t => t.ToFirstUpper()));
        }

        /// <summary>
        /// 切词。
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string[] ToWords(this string source)
        {
            string temp = UpPattern.Replace(source, "_$0");
            return SpanPattern.Replace(temp, "_")
                .Split('_')
                .Where(t => !string.IsNullOrEmpty(t))
                .ToArray();
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
