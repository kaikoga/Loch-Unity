using System;
using JetBrains.Annotations;
using Silksprite.Loch.UIElements.Base;
using UnityEngine.UIElements;

namespace Silksprite.Loch.UIElements.LEditor
{
    [PublicAPI]
    public class ToolbarButton : UnityEditor.UIElements.ToolbarButton, ILocTextElement
    {
        readonly LochPresenter<ToolbarButton> _loch;

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
        void ILochElement.LochRefresh() => _loch.LochRefresh();

        public ToolbarButton()
        {
            _loch = new LochPresenter<ToolbarButton>(this);
            _loch.OnRenderText += str => base.text = str;
        }

        public ToolbarButton(Action clickEvent) : base(clickEvent)
        {
            _loch = new LochPresenter<ToolbarButton>(this);
            _loch.OnRenderText += str => base.text = str;
        }

        public new class UxmlFactory : UxmlFactory<ToolbarButton, UxmlTraits> {}
        public new class UxmlTraits : LochElement.UxmlTraits<UnityEditor.UIElements.ToolbarButton.UxmlTraits> { }
    }
}
