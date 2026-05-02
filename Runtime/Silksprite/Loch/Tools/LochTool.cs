using System;
using System.Reflection;
using JetBrains.Annotations;
using Silksprite.Loch.Core;
using Silksprite.Loch.Core.Reflection;
using UnityEngine;

namespace Silksprite.Loch.Tools
{
    [PublicAPI]
    public static class LochTool
    {
        internal static Assembly? CurrentAssembly;
        
        public static string Tr(string key)
        {
            var assembly = CurrentAssembly ?? Assembly.GetCallingAssembly();
            return LochRepository.Instance.Tr(key, assembly);
        }

        public static string TrFormat(string key, Substitution sub)
        {
            var assembly = CurrentAssembly ?? Assembly.GetCallingAssembly();
            return sub.Format(LochRepository.Instance.Tr(key, assembly));
        }

        public static string LongTr(string key)
        {
            var assembly = CurrentAssembly ?? Assembly.GetCallingAssembly();
            return LochRepository.Instance.LongTr(key, assembly);
        }

        public static string LongTrFormat(string key, Substitution substitution)
        {
            var assembly = CurrentAssembly ?? Assembly.GetCallingAssembly();
            return substitution.Format(LochRepository.Instance.LongTr(key, assembly));
        }

        public static GUIContent GUIContent(string key)
        {
            var assembly = CurrentAssembly ?? Assembly.GetCallingAssembly();
            return LochRepository.Instance.GUIContent(key, assembly);
        }

        public static GUIContent GUIContentFormat(string key, Substitution substitution)
        {
            var assembly = CurrentAssembly ?? Assembly.GetCallingAssembly();
            return new GUIContent(substitution.Format(LochRepository.Instance.Tr(key, assembly)));
        }

        public static string TrEnum<T>(T enumValue)
            where T : Enum
        {
            var assembly = CurrentAssembly ?? Assembly.GetCallingAssembly();
            return LochRepository.Instance.Tr(enumValue, assembly);
        }

        public static GUIContent GUIContentEnum<T>(T enumValue)
            where T : Enum
        {
            var assembly = CurrentAssembly ?? Assembly.GetCallingAssembly();
            return LochRepository.Instance.GUIContent(enumValue, assembly);
        }

        public static LocalizedContent Loc(string key)
        {
            var assembly = CurrentAssembly ?? Assembly.GetCallingAssembly();
            return new LocalizedContent(key, assembly);
        }

        public static LocalizedContent LocEmpty()
        {
            return new LocalizedContent("", Assembly.GetCallingAssembly());
        }

        public static LocalizedContent LocEnum<T>(T enumValue)
            where T : Enum
        {
            return new LocalizedContent(LEnumData.Key(enumValue), Assembly.GetCallingAssembly());
        }

        public static LocalizedContent _Loc(string key)
        {
            var assembly = CurrentAssembly ?? Assembly.GetCallingAssembly();
            return new LocalizedContent(key, assembly);
        }

    }
}