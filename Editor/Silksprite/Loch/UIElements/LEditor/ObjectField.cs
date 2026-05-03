using JetBrains.Annotations;
using Silksprite.Loch.UIElements.Base;
using UnityEngine.UIElements;

namespace Silksprite.Loch.UIElements.LEditor
{
    [PublicAPI]
#if UNITY_2023_2_OR_NEWER
    [UxmlElement] public partial 
#else
    public
#endif
        class ObjectField : UnityEditor.UIElements.ObjectField, ILocField
    {
        readonly LochPresenter<ObjectField> _loch;

        public LocalizedContent? loc
        {
            get => _loch.Loc;
            set => _loch.Loc = value;
        }
        public new string? label
        {
            get => _loch.Label;
            set => _loch.Label = value;
        }
        void ILochElement.LochRefresh() => _loch.LochRefresh();

        public ObjectField()
        {
            _loch = new LochPresenter<ObjectField>(this);
            _loch.OnRenderLabel += str => base.label = str;
        }

#if UNITY_2023_2_OR_NEWER
        [UxmlAttribute("loc")]
        LocalizedContent Loc { get => loc.GetValueOrDefault(); set => loc = value; }

        [UxmlAttribute("label")]
        string Label { get => label ?? ""; set => label = value; }
#else
        public new class UxmlFactory : UxmlFactory<ObjectField, UxmlTraits> {}
        public new class UxmlTraits : LochElement.UxmlTraits<UnityEditor.UIElements.ObjectField.UxmlTraits> { }
#endif
    }
}
