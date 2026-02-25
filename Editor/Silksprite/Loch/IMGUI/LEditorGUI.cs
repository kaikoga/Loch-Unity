using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Silksprite.Loch.Core;
using Silksprite.Loch.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace Silksprite.Loch.IMGUI
{
    [PublicAPI]
    public static class LEditorGUI
    {
        public static void LocaleSelector(Rect position) => LocaleSelector(position, Assembly.GetCallingAssembly());
        public static void LocaleSelector<T>(Rect position) => LocaleSelector(position, typeof(T).Assembly);
        public static void LocaleSelector(Rect position, Assembly assembly)
        {
            var domain = LochRepository.Instance.Get(assembly);
            var localeSet = domain.LocaleSet;
            if (!localeSet.IsSelectable) return;
            using var changed = new EditorGUI.ChangeCheckScope();
            var lang = EditorGUI.Popup(position,
                "Plugin Language",
                Array.IndexOf(localeSet.LocaleCodes, domain.currentLocale.LocaleCode),
                localeSet.LocaleDisplayNames);
            if (changed.changed && lang >= 0)
            {
                domain.SelectLocale(localeSet.Locales[lang]);
            }
        }

        public static void GlobalLocaleSelector(Rect position)
        {
            var localeSet = LochRepository.Instance.localeSet;
            if (!localeSet.IsSelectable) return;
            using var changed = new EditorGUI.ChangeCheckScope();
            var lang = EditorGUI.Popup(position,
                "Plugin(s) Language",
                Array.IndexOf(localeSet.LocaleCodes, LochRepository.Instance.currentLocale.LocaleCode),
                localeSet.LocaleDisplayNames);
            if (changed.changed && lang >= 0)
            {
                LochRepository.Instance.SelectLocale(localeSet.Locales[lang]);
            }
        }

        public static bool HeadingFoldout(Rect position, bool foldout, LocalizedContent loc) =>
            EditorGUI.Foldout(position, foldout, loc.GUIContent, LEditorGUIStyle.HeadingFoldoutStyle);

        public static void Prop(Rect position, LocalizedProperty lop) =>
            Prop(position, lop, lop.GUIContent);
        public static void Prop(Rect position, LocalizedProperty lop, GUIContent label)
            => EditorGUI.PropertyField(position, lop.Property, label);

        public static void PropAsLabel(Rect position, LocalizedProperty lop) =>
            PropAsLabel(position, lop, lop.GUIContent);

        public static void PropAsLabel(Rect position, LocalizedProperty lop, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, lop.Property))
            using (new ShowMixedValueScope(lop))
            {
                EditorGUI.LabelField(position, label, new GUIContent(lop.Property.stringValue));
            }
        }

        public static void PropAsFoldout(Rect position, LocalizedProperty lop, Action? content)
        {
            bool expanded; 
            using (new EditorGUI.PropertyScope(position, lop.GUIContent, lop.Property))
            {
                expanded = EditorGUI.ToggleLeft(position, lop.GUIContent, lop.Property.boolValue);
            }
            lop.Property.boolValue = expanded;
            if (expanded)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    content?.Invoke();
                }
            }
        }

        public static void PropAsEnumPopup<TEnum>(Rect position, LocalizedProperty lop)
            where TEnum : Enum =>
            PropAsEnumPopup<TEnum>(position, lop, lop.GUIContent);

        public static void PropAsEnumPopup<TEnum>(Rect position, LocalizedProperty lop, GUIContent label)
            where TEnum : Enum
        {
            using (new ShowMixedValueScope(lop))
            {
                EditorGUI.BeginChangeCheck();
                TEnum newValue;
                if (LochRepository.Instance.TryLocaleEnumData(typeof(TEnum), lop.Assembly) is { } data)
                {
                    newValue = (TEnum)data.Values[EditorGUI.Popup(position, label, lop.Property.enumValueIndex, data.DisplayGUIContents)];
                }
                else
                {
                    newValue = (TEnum)EditorGUI.EnumPopup(position, label, (TEnum)(object)lop.Property.intValue);
                }

                if (!EditorGUI.EndChangeCheck()) return;
                lop.Property.intValue = Convert.ToInt32(newValue);
            }
        }

        public static T ObjectField<T>(Rect position, LocalizedContent loc, T obj, bool allowSceneObjects)
            where T : UnityEngine.Object =>
            (T) EditorGUI.ObjectField(position, loc.GUIContent, obj, typeof(T), allowSceneObjects);

        public static void LabelField(Rect position, LocalizedContent loc, string value) =>
            EditorGUI.LabelField(position, loc.GUIContent, new GUIContent(value));

        public static int IntField(Rect position, LocalizedContent loc, int value) =>
            EditorGUI.IntField(position, loc.GUIContent, value);
        
        public static bool Toggle(Rect position, LocalizedContent loc, bool value) =>
            EditorGUI.Toggle(position, loc.GUIContent, value);

        public static bool Foldout(Rect position, bool foldout, LocalizedContent loc) =>
            EditorGUI.Foldout(position, foldout, loc.GUIContent);

        public static TEnum EnumPopup<TEnum>(Rect position, LocalizedContent loc, TEnum value)
            where TEnum : Enum =>
            (TEnum)EditorGUI.EnumPopup(position, loc.GUIContent, value);
        
        public static void HelpBox(Rect position, LocalizedContent loc, MessageType type) =>
            EditorGUI.HelpBox(position, loc.LongTr, type);
        public static void HelpBox(Rect position, LocalizedContent loc, MessageType type, Substitution substitution) =>
            EditorGUI.HelpBox(position, loc.LongTrFormat(substitution), type);
    }
}