using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Silksprite.Loch.Core.Reflection;
using Silksprite.Loch.Extensions;
using UnityEngine;

namespace Silksprite.Loch.Core
{
    class Localizer
    {
        readonly ILocalizationProvider? _po;
        public readonly Locale Locale;

        readonly LocalizationCache<string, string> _trCache = new LocalizationCache<string, string>();
        readonly LocalizationCache<string, string> _longTrCache = new LocalizationCache<string, string>();
        readonly LocalizationCache<string, GUIContent> _guiContentCache = new LocalizationCache<string, GUIContent>();
        readonly LocalizationCache<LEnumData, LocaleEnumData> _localeEnumDataCache = new LocalizationCache<LEnumData, LocaleEnumData>();

        public Localizer(ILocalizationProvider? po)
        {
            _po = po;
            Locale = Locale.FromLangCode(po?.LocaleIsoCode);
        }

        public string? TrCachedOrDefault(string key)
        {
            return _trCache.FindOrTryAssign(key,
                () =>
                {
                    if (_po == null) return key.SplitCompat("::").LastOrDefault();
                    if (key == "") return "";
                    var str = _po.GetLocalizedString(key);
                    return key == str ? null : str;
                });
        }

        public string? LongTrCachedOrDefault(string key)
        {
            return _longTrCache.FindOrTryAssign(
                key,
                () => TrCachedOrDefault(key) is { } tr
                    ? Locale.Nowrap ? tr.Nowrap() : tr
                    : null);
        }

        public GUIContent? GUIContentCachedOrDefault(string key)
        {
            return _guiContentCache.FindOrTryAssign(
                key,
                () => TrCachedOrDefault(key) is { } tr
                    ? new GUIContent(tr, null, tr)
                    : null);
        }

        public LocaleEnumData? LocaleEnumDataCachedOrDefault(LEnumData data, Assembly assembly)
        {
            return _localeEnumDataCache.FindOrTryAssign(
                data,
                () => LocaleEnumData.From(data, assembly));
        }

        class LocalizationCache<TKey, TValue>
        where TValue : class
        {
            readonly Dictionary<TKey, TValue?> _cache = new Dictionary<TKey, TValue?>();

            public TValue? FindOrTryAssign(TKey key, Func<TValue?> generator)
            {
                if (_cache.TryGetValue(key, out var value)) return value;
                value = generator();
                _cache.Add(key, value);
                return value;
            }
        }
    }
}
