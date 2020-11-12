using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System
{
    public static class EnumExtensions
    {
        [SuppressMessage("Usage", "CA2248")]
        public static IEnumerable<TEnum> GetFlags<TEnum>(this TEnum flags) where TEnum : struct, Enum =>
            Enum.GetValues<TEnum>().Where(value => flags.HasFlag(value));

        public static String GetName<TEnum>(this TEnum value) where TEnum : struct, Enum =>
            Enum.GetName(value) ?? throw new ArgumentException($"Invalid enum value: {value}", nameof(value));
    }
}