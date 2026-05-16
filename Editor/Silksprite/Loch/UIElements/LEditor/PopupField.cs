using JetBrains.Annotations;
using Silksprite.Loch.UIElements.Base;
using UnityEngine.UIElements;

#if UNITY_2022_3_OR_NEWER
using UnityUIElements = UnityEngine.UIElements;
#else
using UnityUIElements = UnityEditor.UIElements;
#endif

namespace Silksprite.Loch.UIElements.LEditor
{
    [PublicAPI]
#if UNITY_2022_3_OR_NEWER
    [UnityEngine.Scripting.APIUpdating.MovedFrom(false)]
#endif
#if UNITY_2023_2_OR_NEWER
    [UxmlElement] public partial 
#else
    public
#endif
        class PopupField<T> : UnityUIElements.PopupField<T>, ILocField
    {
        readonly LochPresenter<PopupField<T>> _loch;

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

        public PopupField()
        {
            _loch = new LochPresenter<PopupField<T>>(this);
            _loch.OnRenderLabel += str => base.label = str;
        }

#if UNITY_2023_2_OR_NEWER
        [UxmlAttribute("loc")]
        LocalizedContent Loc { get => loc.GetValueOrDefault(); set => loc = value; }

        [UxmlAttribute("label")]
        string Label { get => label ?? ""; set => label = value; }
#else
        public new class UxmlFactory : UxmlFactory<PopupField<T>, UxmlTraits> {}
        public new class UxmlTraits : LochElement.UxmlTraits<UnityUIElements.PopupField<T>.UxmlTraits> { }
#endif
    }
}
