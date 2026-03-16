using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;

namespace Silksprite.Loch.Core.Settings
{
    [UsedImplicitly]
    class EditorPrefsLocaleSettingRepository : ILocaleSettingRepository
    {
        const string LocalesKeyPrefix = "net.kaikoga.Loch.locales";
        const string EnableCSharpLocaleKey = "net.kaikoga.Loch.enableCSharpLocale";

        static string EditorPrefKey(string key) => $"{LocalesKeyPrefix}.{key}";

        public bool EnableCSharpLocale
        {
            get => EditorPrefs.GetBool(EnableCSharpLocaleKey, false);
            set => EditorPrefs.SetBool(EnableCSharpLocaleKey, value);
        }

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
