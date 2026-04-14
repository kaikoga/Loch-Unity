using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;

namespace Silksprite.Loch.Core
{
    public readonly struct Locale
    {
        static readonly Locale Default = new Locale("en-US");
        static readonly Locale CSharp = new Locale("CSharp");

        static readonly Dictionary<string, string> LocaleCodeToLocaleGroup = new Dictionary<string, string>();

        public readonly string LocaleCode;
        internal string LocaleGroup
        {
            get
            {
                if (LocaleCodeToLocaleGroup.TryGetValue(LocaleCode, out var localeGroup))
                {
                    return localeGroup;
                }
                var i = LocaleCode.IndexOf('-');
                localeGroup = i < 0 ? LocaleCode : LocaleCode[..i];
                LocaleCodeToLocaleGroup.Add(LocaleCode, localeGroup);
                return localeGroup;
            }
        }

        string LocaleCodeIgnoreCase => LocaleCode.ToLowerInvariant();
        [UsedImplicitly]
        internal string NdmfCodeInternal => LocaleCodeIgnoreCase;

        public string DisplayName => GuessDisplayName(this);
        public bool Nowrap => GuessNowrap(LocaleCode);

        Locale(string localeCode)
        {
            LocaleCode = Normalize(localeCode);
        }

        internal static Locale FromLocaleCode(string? localeCode) => localeCode != null ? new Locale(localeCode) : CSharp;

        static string Normalize(string langCode) => langCode.Replace("_", "-");
        public static Locale Resolve(string langCode, IReadOnlyCollection<Locale> locales)
        {
            var input = FromLocaleCode(langCode);
            foreach (var locale in locales)
            {
                if (locale.LocaleCode == input.LocaleCode) return locale;
            }
            foreach (var locale in locales)
            {
                if (string.Equals(locale.LocaleCode, input.LocaleCode, StringComparison.OrdinalIgnoreCase)) return locale;
            }
            foreach (var locale in locales)
            {
                if (locale.LocaleGroup == input.LocaleGroup) return locale;
            }
            foreach (var locale in locales)
            {
                if (locale.LocaleCode == Default.LocaleCode) return locale;
            }
            foreach (var locale in locales)
            {
                if (locale.LocaleGroup == Default.LocaleGroup) return locale;
            }
            return Default;
        }

        static string GuessDisplayName(Locale locale)
        {
            if (locale.LocaleCode == CSharp.LocaleCode)
            {
                return "CSharp";
            }
            try
            {
                return CultureInfo.GetCultureInfo(locale.LocaleCode).NativeName;
            }
            catch (Exception)
            {
                return locale.LocaleCode;
            }
        }

        static bool GuessNowrap(string code) =>
            code[..2] switch
            {
                "ja" => true,
                "ko" => true,
                "zh" => true,
                _ => false
            };
    }
}
