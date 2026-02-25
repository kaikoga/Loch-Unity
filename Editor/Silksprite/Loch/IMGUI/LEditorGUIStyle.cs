using UnityEditor;
using UnityEngine;

namespace Silksprite.Loch.IMGUI
{
    static class LEditorGUIStyle
    {
        public static readonly GUIStyle HeadingFoldoutStyle = new GUIStyle(EditorStyles.foldout)
        {
            fontStyle = FontStyle.Bold
        };
    }
}