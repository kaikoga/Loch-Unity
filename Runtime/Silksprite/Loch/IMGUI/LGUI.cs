using JetBrains.Annotations;
using UnityEngine;

namespace Silksprite.Loch.IMGUI
{
    // ReSharper disable once InconsistentNaming
    [PublicAPI]
    public static class LGUI
    {
        public static void Heading(Rect position, LocalizedContent loc)
            => GUI.Label(position, loc.Tr, LGUIStyle.HeadingStyle);

        public static void Label(Rect position, LocalizedContent loc)
            => GUI.Label(position, loc.GUIContent);

        public static void Box(Rect position, LocalizedContent loc)
            => GUI.Box(position, loc.GUIContent);

        public static bool Button(Rect position, LocalizedContent loc)
            => GUI.Button(position, loc.GUIContent);

        public static bool RepeatButton(Rect position, LocalizedContent loc)
            => GUI.RepeatButton(position, loc.GUIContent);

        public static bool Toggle(Rect position, bool value, LocalizedContent loc)
            => GUI.Toggle(position, value, loc.GUIContent);
    }
}