using JetBrains.Annotations;
using UnityEngine;

namespace Silksprite.Loch.IMGUI
{
    // ReSharper disable once InconsistentNaming
    [PublicAPI]
    public static class LGUILayout
    {
        public static void Heading(LocalizedContent loc, params GUILayoutOption[] options)
            => GUILayout.Label(loc.Tr, LGUIStyle.HeadingStyle, options);

        public static void Label(LocalizedContent loc, params GUILayoutOption[] options)
            => GUILayout.Label(loc.GUIContent, options);

        public static void Box(LocalizedContent loc, params GUILayoutOption[] options)
            => GUILayout.Box(loc.GUIContent, options);

        public static bool Button(LocalizedContent loc, params GUILayoutOption[] options)
            => GUILayout.Button(loc.GUIContent, options);

        public static bool RepeatButton(LocalizedContent loc, params GUILayoutOption[] options)
            => GUILayout.RepeatButton(loc.GUIContent, options);

        public static bool Toggle(bool value, LocalizedContent loc, params GUILayoutOption[] options)
            => GUILayout.Toggle(value, loc.GUIContent, options);
    }
}