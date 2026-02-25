using UnityEngine;

namespace Silksprite.Loch.IMGUI
{
    // ReSharper disable once InconsistentNaming
    static class LGUIStyle
    {
        public static readonly GUIStyle HeadingStyle = new GUIStyle
        {
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleLeft,
            padding = new RectOffset(-4, 0, 4, 0)
        };
    }
}