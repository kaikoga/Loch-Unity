using JetBrains.Annotations;
using Silksprite.Loch.UIElements.Base;
using UnityEngine.UIElements;

namespace Silksprite.Loch.UIElements.LEditor
{
    [PublicAPI]
    public class ObjectField : UnityEditor.UIElements.ObjectField, ILocField
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

        public new class UxmlFactory : UxmlFactory<ObjectField, UxmlTraits> {}
        public new class UxmlTraits : LochElement.UxmlTraits<UnityEditor.UIElements.ObjectField.UxmlTraits> { }
    }
}
