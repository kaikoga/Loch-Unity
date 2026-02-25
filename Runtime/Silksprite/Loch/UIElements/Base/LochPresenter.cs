using System;
using UnityEngine.UIElements;

namespace Silksprite.Loch.UIElements.Base
{
    class LochPresenter<T>
    where T : VisualElement, ILochElement
    {
        readonly T _baseElement;

        LocalizedContent? _loc;
        string? _text;
        string? _label;
        bool _isLongTr;

        public event Action<string>? OnRenderText;
        public event Action<string>? OnRenderLabel;

        public LochPresenter(T baseElement)
        {
            _baseElement = baseElement;
            baseElement.RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            baseElement.RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
        }
        
        void OnAttachToPanel(AttachToPanelEvent evt) => LochElementRefresher.Instance.Add(_baseElement);
        void OnDetachFromPanel(DetachFromPanelEvent evt) => LochElementRefresher.Instance.Remove(_baseElement);

        public LocalizedContent? Loc
        {
            get => _loc;
            set
            {
                _loc = value;
                LochRefresh();
            }
        }
        public string? Text
        {
            get => _text;
            set
            {
                _text = value;
                LochRefresh();
            }
        }
        public string? Label
        {
            get => _label;
            set
            {
                _label = value;
                LochRefresh();
            }
        }
        public bool IsLongTr
        {
            get => _isLongTr;
            set
            {
                _isLongTr = value;
                LochRefresh();
            }
        }

        static string Tr(LocalizedContent? loc, string? original)
        {
            return string.IsNullOrWhiteSpace(original) ? "" : loc?.TryTr ?? original ?? "";
        }

        static string LongTr(LocalizedContent? loc, string? original)
        {
            return string.IsNullOrWhiteSpace(original) ? "" : loc?.LongTr ?? original ?? "";
        }

        public void LochRefresh()
        {
            if (IsLongTr)
            {
                OnRenderText?.Invoke(LongTr(_loc, _text));
                OnRenderLabel?.Invoke(LongTr(_loc, _label));
            }
            else
            {
                OnRenderText?.Invoke(Tr(_loc, _text));
                OnRenderLabel?.Invoke(Tr(_loc, _label));
            }
        }
    }
}
