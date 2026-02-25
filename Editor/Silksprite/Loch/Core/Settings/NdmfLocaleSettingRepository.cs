#if LOCH_NDMF_SUPPORT
using System.Collections.Generic;
using System.Linq;
using nadena.dev.ndmf.localization;

namespace Silksprite.Loch.Core.Settings
{
    class NdmfLocaleSettingRepository : ILocaleSettingRepository
    {
        public NdmfLocaleSettingRepository()
        {
            LanguagePrefs.RegisterLanguageChangeCallback(this, _ => LochRepository.Instance.ReloadCurrentLocales());    
        }

        public Locale Resolve(string key, IReadOnlyCollection<Locale> locales)
        {
            var ndmfCode = LanguagePrefs.Language;
            return Locale.Resolve(ndmfCode, locales);
        }

        public void Write(string key, Locale locale)
        {
            var ndmfLocale = Locale.Resolve(locale.LocaleCode, LanguagePrefs.RegisteredLanguages.Select(Locale.FromLangCode).ToArray());
            LanguagePrefs.Language = ndmfLocale.NdmfCode;
        }
    }
}
#endif