using System;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Silksprite.Loch.IMGUI
{
    [PublicAPI]
    public static class LEditorGUILayout
    {
        public static void LocaleSelector(params GUILayoutOption[] options) => LocaleSelector(Assembly.GetCallingAssembly(), options);
        public static void LocaleSelector<T>(params GUILayoutOption[] options) => LocaleSelector(typeof(T).Assembly, options);
        public static void LocaleSelector(Assembly assembly, params GUILayoutOption[] options) =>
            LEditorGUI.LocaleSelector(EditorGUILayout.GetControlRect(options), assembly);
        public static void GlobalLocaleSelector(params GUILayoutOption[] options) =>
            LEditorGUI.GlobalLocaleSelector(EditorGUILayout.GetControlRect(options));

        public static bool HeadingFoldout(bool foldout, LocalizedContent loc) =>
            EditorGUILayout.Foldout(foldout, loc.GUIContent, LEditorGUIStyle.HeadingFoldoutStyle);

        public static void Prop(LocalizedProperty lop, params GUILayoutOption[] options) =>
            EditorGUILayout.PropertyField(lop.Property, lop.Loc.GUIContent, options);
        public static void Prop(LocalizedProperty lop, GUIContent label, params GUILayoutOption[] options) =>
            EditorGUILayout.PropertyField(lop.Property, label, options);

        public static void PropAsLabel(LocalizedProperty lop, params GUILayoutOption[] options) =>
            LEditorGUI.PropAsLabel(EditorGUILayout.GetControlRect(options), lop, lop.GUIContent);
        public static void PropAsLabel(LocalizedProperty lop, GUIContent label, params GUILayoutOption[] options) =>
            LEditorGUI.PropAsLabel(EditorGUILayout.GetControlRect(options), lop, label);

        public static void PropAsFoldout(LocalizedProperty lop, Action? content = null, params GUILayoutOption[] options) =>
            LEditorGUI.PropAsFoldout(EditorGUILayout.GetControlRect(options), lop, content);

        public static void PropAsEnumPopup<TEnum>(LocalizedProperty lop, params GUILayoutOption[] options)
            where TEnum : Enum =>
            LEditorGUI.PropAsEnumPopup<TEnum>(EditorGUILayout.GetControlRect(options), lop);

        public static T ObjectField<T>(LocalizedContent loc, T? obj, bool allowSceneObjects, params GUILayoutOption[] options)
            where T : UnityEngine.Object =>
            (T) EditorGUILayout.ObjectField(loc.GUIContent, obj, typeof(T), allowSceneObjects, options);

        public static void LabelField(LocalizedContent loc, params GUILayoutOption[] options) =>
            EditorGUILayout.LabelField(loc.GUIContent, options);

        public static void LabelField(LocalizedContent loc, string value, params GUILayoutOption[] options) =>
            EditorGUILayout.LabelField(loc.GUIContent, new GUIContent(value), options);

        public static int IntField(LocalizedContent loc, int value, params GUILayoutOption[] options) =>
            EditorGUILayout.IntField(loc.GUIContent, value, options);
        
        public static float FloatField(LocalizedContent loc, float value, params GUILayoutOption[] options) =>
            EditorGUILayout.FloatField(loc.GUIContent, value, options);
        
        public static string TextField(LocalizedContent loc, string value, params GUILayoutOption[] options) =>
            EditorGUILayout.TextField(loc.GUIContent, value, options);
        
        public static bool Toggle(LocalizedContent loc, bool value, params GUILayoutOption[] options) =>
            EditorGUILayout.Toggle(loc.GUIContent, value, options);

        public static bool Foldout(bool foldout, LocalizedContent loc) =>
            EditorGUILayout.Foldout(foldout, loc.GUIContent);

        public static TEnum EnumPopup<TEnum>(LocalizedContent loc, TEnum value, params GUILayoutOption[] options)
            where TEnum : Enum =>
            (TEnum)EditorGUILayout.EnumPopup(loc.GUIContent, value, options);
        
        public static void HelpBox(LocalizedContent loc, MessageType type) =>
            EditorGUILayout.HelpBox(loc.LongTr, type);
        public static void HelpBox(LocalizedContent loc, MessageType type, Substitution substitution) =>
            EditorGUILayout.HelpBox(loc.LongTrFormat(substitution), type);
    }
}