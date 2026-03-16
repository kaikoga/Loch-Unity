using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Silksprite.Loch.Core.Reflection;
using Silksprite.Loch.Core.Settings;
using UnityEngine;

namespace Silksprite.Loch.Core
{
    public class LochDomain
    {
        static readonly Localizer EmptyLocalizer = new Localizer(null);

        readonly string _key;
        readonly Localizer[] _locales;
        readonly HashSet<string> _assemblies;
        readonly string[] _assemblyPrefixes;
        internal readonly LocaleSet LocaleSet;
        Localizer _currentLocalizer;

        internal Locale CurrentLocale
        {
            get => _currentLocalizer.Locale;
            set => SelectLocale(value);
        }

        internal void SelectLocale(Locale value)
        {
            if (LocaleSettingRepository.Instance.Resolve(_key, LocaleSet.Locales).LocaleCode == value.LocaleCode)
            {
                return;
            }
            LocaleSettingRepository.Instance.Write(_key, value);
        }

        public LochDomain(string key, IEnumerable<ILocalizationProvider> po, IEnumerable<string> assemblies, IEnumerable<string> assemblyPrefixes)
        {
            _key = key;
            _locales = po.Select(p => new Localizer(p)).ToArray();
            _assemblies = assemblies.ToHashSet();
            _assemblyPrefixes = assemblyPrefixes.ToArray();
            LocaleSet = new LocaleSet(_locales.Select(locale => locale.Locale));
            _currentLocalizer = null!;
            LoadCurrentLocale();
        }

        internal void LoadCurrentLocale()
        {
            var currentLocaleCode = LocaleSettingRepository.Instance.Resolve(_key, LocaleSet.Locales).LocaleCode;
            _currentLocalizer = _locales.FirstOrDefault(locale => locale.Locale.LocaleCode == currentLocaleCode) ?? EmptyLocalizer;
        }

        internal bool Match(string asmName) => _assemblies.Contains(asmName);

        internal bool WeakMatch(string asmName) => _assemblyPrefixes.Any(asmName.StartsWith);

        internal string? TryTrCached(string key) => _currentLocalizer.TrCachedOrDefault(key);

        internal string? TryLongTrCached(string key) => _currentLocalizer.LongTrCachedOrDefault(key);

        internal GUIContent? TryGUIContentCached(string key) => _currentLocalizer.GUIContentCachedOrDefault(key);

        internal LocaleEnumData? TryLocaleEnumData(LEnumData data, Assembly assembly) => _currentLocalizer.LocaleEnumDataCachedOrDefault(data, assembly);
    }
}
