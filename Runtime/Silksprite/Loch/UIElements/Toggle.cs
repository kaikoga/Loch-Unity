using JetBrains.Annotations;
using Silksprite.Loch.UIElements.Base;
using UnityEngine.UIElements;

namespace Silksprite.Loch.UIElements
{
    [PublicAPI]
    public class Toggle : UnityEngine.UIElements.Toggle, ILocTextElement, ILocField
    {
        readonly LochPresenter<Toggle> _loch;

        public LocalizedContent? loc
        {
            get => _loch.Loc;
            set => _loch.Loc = value;
        }
        public new string? text
        {
            get => _loch.Text;
            set => _loch.Text = value;
        }
        public new string? label
        {
            get => _loch.Label;
            set => _loch.Label = value;
        }
        void ILochElement.LochRefresh() => _loch.LochRefresh();

        public Toggle()
        {
            _loch = new LochPresenter<Toggle>(this);
            _loch.OnRenderText += str => base.text = str;
            _loch.OnRenderLabel += str => base.label = str;
        }

        public new class UxmlFactory : UxmlFactory<Toggle, UxmlTraits> {}
        public new class UxmlTraits : LochElement.UxmlTraits<UnityEngine.UIElements.Toggle.UxmlTraits> { }
    }
}
