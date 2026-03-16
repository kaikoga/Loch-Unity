using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Silksprite.Loch.Core.Reflection;
using UnityEngine;

namespace Silksprite.Loch.Core
{
    public class LochRepository
    {
        public static readonly LochRepository Instance = new LochRepository();

        static LochDomain Fallback(Assembly assembly)
        {
            var name = assembly.GetName().Name;
            return new LochDomain(
                name,
                Enumerable.Empty<ILocalizationProvider>(),
                new [] { name },
                new [] { name });
        }

        readonly List<LochDomain> _domains = new List<LochDomain>();
        LocaleSet? _localeSet;

        internal LocaleSet LocaleSet
        {
            get
            {
                if (_localeSet != null)
                {
                    return _localeSet;
                }
                var locales = _domains
                    .SelectMany(domain => domain.LocaleSet.Locales)
                    .GroupBy(locale => locale.LocaleCode, locale => locale)
                    .SelectMany(group => group.Take(1))
                    .ToArray();
                var generalizedLanguageCodes = locales
                    .Where(locale => locale.LocaleCode != locale.LocaleGroup)
                    .Select(locale => locale.LocaleGroup)
                    .ToHashSet();
                var filteredLocales = locales
                    .Where(locale => locale.LocaleCode != locale.LocaleGroup && generalizedLanguageCodes.Contains(locale.LocaleGroup)); 
                _localeSet = new LocaleSet(filteredLocales);
                return _localeSet;
            }
        }

        internal Locale CurrentLocale
        {
            get => _domains.First().CurrentLocale;
            set => SelectLocale(value);
        }

        internal void SelectLocale(Locale value)
        {
            foreach (var domain in _domains)
            {
                domain.SelectLocale(value);                
            }
        }

        public event Action? OnLocaleChanged;

        LochRepository()
        {
        }

        public void Add(LochDomain lochDomain)
        {
            _domains.Add(lochDomain);
            _localeSet = null;
        }

        internal LochDomain Get(Assembly assembly)
        {
            var asmName = assembly.GetName().Name;
            return _domains.FirstOrDefault(domain => domain.Match(asmName))
                   ?? _domains.FirstOrDefault(domain => domain.WeakMatch(asmName))
                   ?? Fallback(assembly);
        }

        internal string Tr(string key, Assembly assembly) => TryTr(key, assembly) ?? $"<{key} {assembly.GetName().Name}>";
        internal string? TryTr(string key, Assembly assembly) => Get(assembly).TryTrCached(key);

        internal string LongTr(string key, Assembly assembly) => TryLongTr(key, assembly) ?? $"<{key} {assembly.GetName().Name}>";
        internal string? TryLongTr(string key, Assembly assembly) => Get(assembly).TryLongTrCached(key);

        internal GUIContent GUIContent(string key, Assembly assembly) => TryGUIContent(key, assembly) ?? new GUIContent($"<{key} {assembly.GetName().Name}>");
        GUIContent? TryGUIContent(string key, Assembly assembly) => Get(assembly).TryGUIContentCached(key);

        LocaleEnumData? TryLocaleEnumData(LEnumData data, Assembly assembly) => Get(assembly).TryLocaleEnumData(data, assembly);

        internal LocaleEnumData? TryLocaleEnumData(Type enumType, Assembly assembly) =>
            LEnumRepository.Instance.TryGetData(enumType, out var data)
                ? TryLocaleEnumData(data, assembly)
                : null;

        internal string Tr<T>(T enumValue, Assembly assembly) where T : Enum =>
            TryTr(enumValue, assembly) ?? $"<{enumValue.GetType().Name}::{enumValue} {assembly.GetName().Name}>";
        string? TryTr<T>(T enumValue, Assembly assembly) where T : Enum =>
            LEnumRepository.Instance.TryGetData(enumValue.GetType(), out _)
                ? TryTr(LEnumData.Key(enumValue), assembly)
                : null;

        internal GUIContent GUIContent<T>(T enumValue, Assembly assembly) where T : Enum =>
            TryGUIContent(enumValue, assembly) ?? new GUIContent($"<{enumValue.GetType().Name}::{enumValue} {assembly.GetName().Name}>");
        GUIContent? TryGUIContent<T>(T enumValue, Assembly assembly) where T : Enum =>
            LEnumRepository.Instance.TryGetData(enumValue.GetType(), out _)
                ? TryGUIContent(LEnumData.Key(enumValue), assembly)
                : null;

        public void ClearAllCaches()
        {
            _domains.Clear();
        }

        internal void ReloadCurrentLocales()
        {
            foreach (var domain in _domains)
            {
                domain.LoadCurrentLocale();
            }
            OnLocaleChanged?.Invoke();
        }
    }
}
