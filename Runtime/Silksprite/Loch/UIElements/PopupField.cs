#if UNITY_2022_3_OR_NEWER

using JetBrains.Annotations;
using Silksprite.Loch.UIElements.Base;
using UnityEngine.UIElements;
using UnityUIElements = UnityEngine.UIElements;

namespace Silksprite.Loch.UIElements
{
    [PublicAPI]
    public class PopupField<T> : UnityUIElements.PopupField<T>, ILocField
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

        public new class UxmlFactory : UxmlFactory<PopupField<T>, UxmlTraits> {}
        public new class UxmlTraits : LochElement.UxmlTraits<UnityUIElements.PopupField<T>.UxmlTraits> { }
    }
}

#endif