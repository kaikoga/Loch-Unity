using JetBrains.Annotations;

namespace Silksprite.Loch.Extensions
{
    [PublicAPI]
    public static class LochExtension
    {
        public static string Nowrap(this string str) => str.Replace(" ", " ");

#if UNITY_2022_3_OR_NEWER

        internal static string[] SplitCompat(this string str, string separator) => str.Split(separator);

#else

        internal static string[] SplitCompat(this string str, string separator) => str.Split(new [] { separator }, System.StringSplitOptions.None);

#endif
    }
}