using System.Reflection;
using JetBrains.Annotations;
using Silksprite.Loch.Core.Reflection;
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
        class LocaleSelector : IMGUIContainer
    {
        Assembly _assembly;

        public LocaleSelector() : this(Assembly.GetCallingAssembly())
        {
        }

        public LocaleSelector(Assembly assembly)
        {
            _assembly = assembly;
            style.minHeight = EditorGUIUtility.singleLineHeight;
            style.marginLeft = 2;
            style.marginRight = 2;
            style.marginTop = 2;
            style.marginBottom = 2;
            onGUIHandler = OnGUI;
        }

        void OnGUI()
        {
            LEditorGUILayout.LocaleSelector(_assembly);
        }

#if !UNITY_2023_2_OR_NEWER
        public new class UxmlFactory : UxmlFactory<LocaleSelector, UxmlTraits> {}
        
        public new class UxmlTraits : IMGUIContainer.UxmlTraits
        {
            readonly UxmlStringAttributeDescription _type = new UxmlStringAttributeDescription { name = "type" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var maybeTypeName = ""; 
                if (!_type.TryGetValueFromBag(bag, cc, ref maybeTypeName))
                {
                    maybeTypeName = cc.visualTreeAsset.name;
                }

                ((LocaleSelector)ve)._assembly = TypeRepository.Instance.GetType(maybeTypeName)?.Assembly ?? Assembly.GetCallingAssembly();
            }
        }
#endif
    }
}
