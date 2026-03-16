using System.Collections.Generic;
using UnityEngine;

namespace Silksprite.Loch.Core.Settings
{
    class PlayerPrefsLocaleSettingRepository : ILocaleSettingRepository
    {
        const string LocalesKeyPrefix = "net.kaikoga.Loch.locales";
        const string EnableCSharpLocaleKey = "net.kaikoga.Loch.enableCSharpLocale";

        static string PlayerPrefKey(string key) => $"{LocalesKeyPrefix}.{key}";
        
        public bool EnableCSharpLocale
        {
            get => PlayerPrefs.GetInt(EnableCSharpLocaleKey, 0) != 0;
            set => PlayerPrefs.SetInt(EnableCSharpLocaleKey, value ? 1 : 0);
        }

        public Locale Resolve(string key, IReadOnlyCollection<Locale> locales)
        {
            var langCode = PlayerPrefs.GetString(PlayerPrefKey(key), "");
            return Locale.Resolve(langCode, locales);
        }

        public void Write(string key, Locale locale)
        {
            PlayerPrefs.SetString(PlayerPrefKey(key), locale.LocaleCode);
            LochRepository.Instance.ReloadCurrentLocales();
        }
    }
}
