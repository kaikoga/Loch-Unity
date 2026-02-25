using JetBrains.Annotations;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.Loch.UIElements.LEditor
{
    [PublicAPI]
    public class GlobalLocaleSelector : IMGUIContainer
    {
        public GlobalLocaleSelector()
        {
            style.minHeight = EditorGUIUtility.singleLineHeight;
            style.marginLeft = 2;
            style.marginRight = 2;
            style.marginTop = 2;
            style.marginBottom = 2;
            onGUIHandler = OnGUI;
        }

        void OnGUI()
        {
            LEditorGUILayout.GlobalLocaleSelector();
        }

        public new class UxmlFactory : UxmlFactory<GlobalLocaleSelector, UxmlTraits> {}
        
        public new class UxmlTraits : IMGUIContainer.UxmlTraits {}
    }
}
