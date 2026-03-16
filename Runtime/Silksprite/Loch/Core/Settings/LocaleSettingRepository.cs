using System.Collections.Generic;

namespace Silksprite.Loch.Core.Settings
{
    static class LocaleSettingRepository
    {
        static ILocaleSettingRepository _instance = new PlayerPrefsLocaleSettingRepository();
        internal static ILocaleSettingRepository Instance
        {
            get => _instance;
            set
            {
                _instance = value;
                LochRepository.Instance.ReloadCurrentLocales();
            }
        }
    }

    interface ILocaleSettingRepository
    {
        bool EnableCSharpLocale { get; set; }

        Locale Resolve(string key, IReadOnlyCollection<Locale> locales);
        void Write(string key, Locale locale);
    }
}