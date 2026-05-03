using JetBrains.Annotations;
using Silksprite.Loch.UIElements.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace Silksprite.Loch.UIElements
{
    /// <summary>
    /// Simple helper to render a header-like bold text through IMGUI to avoid UIElements font issue
    /// </summary>
    [PublicAPI]
#if UNITY_2023_2_OR_NEWER
    [UxmlElement] public partial 
#else
    public
#endif
        class Heading : IMGUIContainer, ILocTextElement
    {
        static readonly GUIStyle HeadingStyle = new GUIStyle
        {
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleLeft,
            padding = new RectOffset(0, 0, 4, 0)
        };

        readonly LochPresenter<Heading> _loch;

        public LocalizedContent? loc
        {
            get => _loch.Loc;
            set => _loch.Loc = value;
        }
        public string? text
        {
            get => _loch.Text;
            set => _loch.Text = value;
        }

        void ILochElement.LochRefresh() => _loch.LochRefresh();

        string _text = "";

        public Heading()
        {
            onGUIHandler = OnGUI;
            _loch = new LochPresenter<Heading>(this);
            _loch.OnRenderText += str => _text = str;
        }

        void OnGUI()
        {
            GUILayout.Label(_text, HeadingStyle);
        }

#if UNITY_2023_2_OR_NEWER
        [UxmlAttribute("loc")]
        LocalizedContent Loc { get => loc.GetValueOrDefault(); set => loc = value; }

        [UxmlAttribute("text")]
        string Text { get => text ?? ""; set => text = value; }
#else
        public new class UxmlFactory : UxmlFactory<Heading, UxmlTraits> {}
        public new class UxmlTraits : LochElement.UxmlTraits<UnityEngine.UIElements.IMGUIContainer.UxmlTraits> { }
#endif
    }
}
