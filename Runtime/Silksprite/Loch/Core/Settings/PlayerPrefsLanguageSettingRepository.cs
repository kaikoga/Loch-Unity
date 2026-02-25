using System.Collections.Generic;
using UnityEngine;

namespace Silksprite.Loch.Core.Settings
{
    class PlayerPrefsLocaleSettingRepository : ILocaleSettingRepository
    {
        static string EditorPrefKey(string key) => "net.kaikoga.Loch.locales." + key;

        public Locale Resolve(string key, IReadOnlyCollection<Locale> locales)
        {
            var langCode = PlayerPrefs.GetString(EditorPrefKey(key), "");
            return Locale.Resolve(langCode, locales);
        }

        public void Write(string key, Locale locale)
        {
            PlayerPrefs.SetString(EditorPrefKey(key), locale.LocaleCode);
            LochRepository.Instance.ReloadCurrentLocales();
        }
    }
}
