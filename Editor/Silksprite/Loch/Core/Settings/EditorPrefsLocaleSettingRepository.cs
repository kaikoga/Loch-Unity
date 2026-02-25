using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;

namespace Silksprite.Loch.Core.Settings
{
    [UsedImplicitly]
    class EditorPrefsLocaleSettingRepository : ILocaleSettingRepository
    {
        static string EditorPrefKey(string key) => "net.kaikoga.Loch.locales." + key;

        public Locale Resolve(string key, IReadOnlyCollection<Locale> locales)
        {
            var langCode = EditorPrefs.GetString(EditorPrefKey(key), "");
            return Locale.Resolve(langCode, locales);
        }

        public void Write(string key, Locale locale)
        {
            EditorPrefs.SetString(EditorPrefKey(key), locale.LocaleCode);
            LochRepository.Instance.ReloadCurrentLocales();
        }
    }
}
