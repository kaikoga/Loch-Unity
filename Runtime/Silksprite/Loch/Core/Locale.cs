using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Silksprite.Loch.Core
{
    public readonly struct Locale
    {
        static readonly Locale Default = new Locale("en-US");

        public readonly string LocaleCode;

        public string NdmfCode => LocaleCode.ToLowerInvariant();
        public string LanguageCode => LocaleCode.Split("-", 2).FirstOrDefault() ?? "";

        public string DisplayName => GuessDisplayName(LocaleCode);
        public bool Nowrap => GuessNowrap(LocaleCode);

        Locale(string langCode) => LocaleCode = Normalize(langCode);

        public static Locale FromLangCode(string? langCode) => langCode != null ? new Locale(langCode) : Default;

        static string Normalize(string langCode) => langCode.Replace("_", "-");
        public static Locale Resolve(string langCode, IReadOnlyCollection<Locale> locales)
        {
            var input = FromLangCode(langCode);
            foreach (var locale in locales)
            {
                if (locale.LocaleCode == input.LocaleCode) return locale;
            }
            var inNdmfCode = input.NdmfCode;
            foreach (var locale in locales)
            {
                if (locale.NdmfCode == inNdmfCode) return locale;
            }
            var inLanguageCode = input.LanguageCode;
            foreach (var locale in locales)
            {
                if (locale.LanguageCode == inLanguageCode) return locale;
            }
            foreach (var locale in locales)
            {
                if (locale.LocaleCode == Default.LocaleCode) return locale;
            }
            var defaultLanguageCode = Default.LanguageCode;
            foreach (var locale in locales)
            {
                if (locale.LanguageCode == defaultLanguageCode) return locale;
            }
            return Default;
        }

        static string GuessDisplayName(string code)
        {
            try
            {
                return CultureInfo.GetCultureInfo(code).NativeName;
            }
            catch (Exception)
            {
                return code;
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
