using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System
{
    public static class EnumExtensions
    {
        [SuppressMessage("Usage", "CA2248")]
        public static IEnumerable<TEnum> GetFlags<TEnum>(this TEnum flags) where TEnum : struct, Enum
        {
            foreach (var value in Enum.GetValues<TEnum>())
            {
                if (flags.HasFlag(value))
                {
                    yield return value;
                }
            }
        }

        public static String GetName<TEnum>(this TEnum value) where TEnum : struct, Enum =>
            Enum.GetName(value) ?? throw new ArgumentException($"Invalid enum value: {value}", nameof(value));
    }
}