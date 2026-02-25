using UnityEngine;

namespace Silksprite.Loch.Core
{
    public class LocalizationProvider : ILocalizationProvider
    {
        readonly LocalizationAsset _po;

        public string LocaleIsoCode => _po.localeIsoCode;

        public string GetLocalizedString(string original) => _po.GetLocalizedString(original);

        public LocalizationProvider(LocalizationAsset po) => _po = po;
    }
}
