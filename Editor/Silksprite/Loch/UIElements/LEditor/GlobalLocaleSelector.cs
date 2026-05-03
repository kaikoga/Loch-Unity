using JetBrains.Annotations;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.Loch.UIElements.LEditor
{
    [PublicAPI]
#if UNITY_2023_2_OR_NEWER
    [UxmlElement] public partial 
#else
    public
#endif
        class GlobalLocaleSelector : IMGUIContainer
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

#if !UNITY_2023_2_OR_NEWER
        public new class UxmlFactory : UxmlFactory<GlobalLocaleSelector, UxmlTraits> {}
        
        public new class UxmlTraits : IMGUIContainer.UxmlTraits {}
#endif
    }
}
